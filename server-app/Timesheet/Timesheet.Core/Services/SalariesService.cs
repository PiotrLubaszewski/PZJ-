using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Timesheet.Core.Exceptions;
using Timesheet.Core.Extensions;
using Timesheet.Core.Interfaces.Services;
using Timesheet.Core.Models.Collections.Interfaces;
using Timesheet.Core.Models.Helpers;
using Timesheet.Core.Models.Salaries;
using Timesheet.Persistence;
using Timesheet.Persistence.Entities;

namespace Timesheet.Core.Services
{
    public class SalariesService : ISalariesService
    {
        private readonly TimesheetContext _context;

        public SalariesService(TimesheetContext context)
        {
            _context = context;
        }

        public async Task<ICollectionResult<SalaryModel>> GetUserSalariesAsync(string userId, OperationQuery operationQuery, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(userId, out var guid) || !await _context.Users.AnyAsync(x => x.Id == guid, cancellationToken))
                throw new InvalidValidationException(nameof(userId), $"'{nameof(userId).ToPascalCase().InsertSpaces()}' is invalid.");

            return await _context.Salaries.Where(x => x.UserId == guid).ToCollectionResultAsync<Salary, SalaryModel>(operationQuery, cancellationToken);
        }

        public async Task<SalaryModel> GetUserSalaryByIdAsync(string userId, int salaryId, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(userId, out var guid) || !await _context.Users.AnyAsync(x => x.Id == guid, cancellationToken))
                throw new InvalidValidationException(nameof(userId), $"'{nameof(userId).ToPascalCase().InsertSpaces()}' is invalid.");

            if (salaryId < 1)
                throw new InvalidValidationException(nameof(salaryId), $"'{nameof(salaryId).ToPascalCase().InsertSpaces()}' is invalid.");

            var salary = await _context.Salaries.SingleOrDefaultAsync(x => x.Id == salaryId);

            if (salary == null)
                throw new InvalidOperationException($"Salary with given '{nameof(salaryId).ToPascalCase().InsertSpaces()}' does not exist.");

            if (salary.UserId != guid)
                throw new InvalidOperationException($"'{nameof(salaryId).ToPascalCase().InsertSpaces()}' is not assigned to given user.");

            return salary.Adapt<SalaryModel>();
        }

        public async Task<SalaryModel> GetCurrentUserSalaryAsync(string userId, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(userId, out var guid) || !await _context.Users.AnyAsync(x => x.Id == guid, cancellationToken))
                throw new InvalidValidationException(nameof(userId), $"'{nameof(userId).ToPascalCase().InsertSpaces()}' is invalid.");

            var salary = await _context.Salaries.FirstOrDefaultAsync(x => x.UserId == guid && x.StartDateTime >= DateTime.Today && (!x.EndDateTime.HasValue || x.EndDateTime.Value.Date <= DateTime.Today), cancellationToken);

            if (salary == null)
                throw new InvalidOperationException($"Given user does not have any assigned salaries.");

            return salary.Adapt<SalaryModel>();
        }

        public async Task<SalaryModel> GetLastUserSalaryAsync(string userId, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(userId, out var guid) || !await _context.Users.AnyAsync(x => x.Id == guid, cancellationToken))
                throw new InvalidValidationException(nameof(userId), $"'{nameof(userId).ToPascalCase().InsertSpaces()}' is invalid.");

            var salary = await _context.Salaries.FirstOrDefaultAsync(x => x.UserId == guid && !x.EndDateTime.HasValue, cancellationToken);

            if (salary == null)
                throw new InvalidOperationException($"Given user does not have any assigned salaries.");

            return salary.Adapt<SalaryModel>();
        }

        public async Task AddSalaryAsync(string userId, AddSalaryModel model, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(userId, out var guid) || !await _context.Users.AnyAsync(x => x.Id == guid, cancellationToken))
                throw new InvalidValidationException(nameof(userId), $"'{nameof(userId).ToPascalCase().InsertSpaces()}' is invalid.");

            var oldSalaries = await _context.Salaries.Where(x => x.UserId == guid).ToListAsync(cancellationToken);
            if (oldSalaries.Any())
            {
                if (oldSalaries.Any(x => x.StartDateTime.Date >= model.StartDateTime.Date))
                    throw new InvalidValidationException(nameof(model.StartDateTime), $"This user has already a salary with given or later '{nameof(model.StartDateTime).ToPascalCase().InsertSpaces()}'.");

                var last = oldSalaries.OrderByDescending(x => x.StartDateTime).First();
                last.EndDateTime = model.StartDateTime.Date.AddDays(-1);
            }

            await _context.Salaries.AddAsync(new Salary
            {
                UserId = guid,
                CreatedDateTime = DateTime.Now,
                StartDateTime = model.StartDateTime.Date,
                EndDateTime = null,
                Amount = model.Amount,
                MinimumTimeRate = model.MinimumTimeRate,
                OvertimeRate = model.OvertimeRate,
                FineRate = model.FineRate
            },
            cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateSalaryModelAsync(string userId, UpdateSalaryModel model, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(userId, out var guid) || !await _context.Users.AnyAsync(x => x.Id == guid, cancellationToken))
                throw new InvalidValidationException(nameof(userId), $"'{nameof(userId).ToPascalCase().InsertSpaces()}' is invalid.");

            var salary = await _context.Salaries.SingleOrDefaultAsync(x => x.Id == model.Id);

            if (salary == null)
                throw new InvalidOperationException($"Salary with given '{nameof(model.Id).ToPascalCase().InsertSpaces()}' does not exist.");

            if (salary.UserId != guid)
                throw new InvalidOperationException($"'{nameof(model.Id).ToPascalCase().InsertSpaces()}' is not assigned to given user.");

            // TODO: Throw an exception if there are any worked hours with this salary

            salary.Amount = model.Amount;
            salary.MinimumTimeRate = model.MinimumTimeRate;
            salary.OvertimeRate = model.OvertimeRate;
            salary.FineRate = model.FineRate;

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteSalaryAsync(string userId, int salaryId, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(userId, out var guid) || !await _context.Users.AnyAsync(x => x.Id == guid, cancellationToken))
                throw new InvalidValidationException(nameof(userId), $"'{nameof(userId).ToPascalCase().InsertSpaces()}' is invalid.");

            if (salaryId < 1)
                throw new InvalidValidationException(nameof(salaryId), $"'{nameof(salaryId).ToPascalCase().InsertSpaces()}' is invalid.");

            var salary = await _context.Salaries.SingleOrDefaultAsync(x => x.Id == salaryId);

            if (salary == null)
                throw new InvalidOperationException($"Salary with given '{nameof(salaryId).ToPascalCase().InsertSpaces()}' does not exist.");

            if (salary.UserId != guid)
                throw new InvalidOperationException($"'{nameof(salaryId).ToPascalCase().InsertSpaces()}' is not assigned to given user.");

            _context.Salaries.Remove(salary);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
