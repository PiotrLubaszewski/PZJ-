using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
                MinimalTimeRate = model.MinimalTimeRate,
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
            salary.MinimalTimeRate = model.MinimalTimeRate;
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

        public async Task<MonthSalaryModel> GetMonthSalaryAsync(string userId, int year, int month, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(userId, out var guid) || !await _context.Users.AnyAsync(x => x.Id == guid, cancellationToken))
                throw new InvalidValidationException(nameof(userId), $"'{nameof(userId).ToPascalCase().InsertSpaces()}' is invalid.");

            if (year < 2000)
                throw new InvalidValidationException(nameof(year), $"'{nameof(year).ToPascalCase().InsertSpaces()}' is invalid.");

            if (month < 1)
                throw new InvalidValidationException(nameof(month), $"'{nameof(month).ToPascalCase().InsertSpaces()}' is invalid.");

            var startDate = new DateTime(year, month, day: 1).Date;
            var endDate = (new DateTime(year, month + 1, day: 1) - TimeSpan.FromDays(1)).Date;
            var numberOfDays = (endDate - startDate).Days + 1;

            var salary = await _context.Salaries.FirstOrDefaultAsync(x => x.UserId == guid && (x.StartDateTime.Date == startDate || (x.StartDateTime.Date < startDate && !x.EndDateTime.HasValue)), cancellationToken);
            if (salary == null) throw new InvalidValidationException(nameof(month), "This user doesn't have assinged salary for this month.");

            var workedHours = await _context.WorkedHours
                .Where(x => x.UserId == guid && x.Date.Date >= startDate && x.Date.Date <= endDate)
                .ToListAsync(cancellationToken);

            var monthDaySalaryModels = new List<MonthDaySalaryModel>();
            var fullTimeDailyHours = 8m;

            for (int i = 0; i < numberOfDays; i++)
            {
                var currDate = (startDate + TimeSpan.FromDays(i)).Date;

                var fullTimeHoursThisDay = 0m;
                var predictedHoursThisDay = 0m;
                var workedHoursThisDay = workedHours.Where(x => x.Date.Date == currDate).Sum(x => x.HoursQuantity);

                if (currDate.DayOfWeek != DayOfWeek.Saturday && currDate.DayOfWeek != DayOfWeek.Sunday)
                {
                    fullTimeHoursThisDay = fullTimeDailyHours;
                    predictedHoursThisDay = decimal.Round(fullTimeDailyHours * (salary.MinimalTimeRate / 100), 2, MidpointRounding.AwayFromZero);
                }

                monthDaySalaryModels.Add(new MonthDaySalaryModel
                {
                    Date = currDate,
                    FullTimeHours = fullTimeHoursThisDay,
                    PredictedHours = predictedHoursThisDay,
                    WorkedHours = workedHoursThisDay
                });
            }

            var result = new MonthSalaryModel
            {
                Date = startDate,
                Days = monthDaySalaryModels
            };

            var minimalHoursThisMonth = result.TotalFullTimeHours * salary.MinimalTimeRate / 100;
            var oneHourSalaryValue = decimal.Round(salary.Amount / result.TotalFullTimeHours, 2, MidpointRounding.AwayFromZero);

            if (result.TotalWorkedHours < minimalHoursThisMonth)
            {
                var minimalHoursSalary = salary.Amount * salary.MinimalTimeRate / 100;
                var missingHours = decimal.Round(minimalHoursThisMonth - result.TotalWorkedHours, 2, MidpointRounding.AwayFromZero);
                var oneHourSalaryValueWithFine = oneHourSalaryValue * (100 + salary.FineRate) / 100;

                result.SalaryAmount = decimal.Round(minimalHoursSalary - (missingHours * oneHourSalaryValueWithFine), 2, MidpointRounding.AwayFromZero);
            }
            else if (result.TotalWorkedHours > result.TotalFullTimeHours)
            {
                var fullTimeHoursSalary = salary.Amount;
                var extraHours = decimal.Round(result.TotalWorkedHours - result.TotalFullTimeHours, 2, MidpointRounding.AwayFromZero);
                var oneHourSalaryValueWithOvertimeRate = oneHourSalaryValue * (100 + salary.OvertimeRate) / 100;

                result.SalaryAmount = decimal.Round(fullTimeHoursSalary + (extraHours * oneHourSalaryValueWithOvertimeRate), 2, MidpointRounding.AwayFromZero);
            }
            else
            {
                result.SalaryAmount = decimal.Round(result.TotalWorkedHoursRatio * salary.Amount, 2, MidpointRounding.AwayFromZero);
            }

            return result;
        }
    }
}
