using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Timesheet.Core.Exceptions;
using Timesheet.Core.Extensions;
using Timesheet.Core.Interfaces.Services;
using Timesheet.Core.Models.WorkedHours;
using Timesheet.Persistence;
using Timesheet.Persistence.Entities;

namespace Timesheet.Core.Services
{
    public class WorkedHoursService : IWorkedHoursService
    {
        private readonly TimesheetContext _context;

        public WorkedHoursService(TimesheetContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<WorkedHourGroupedModel>> GetWorkedHoursAsync(string userId, GetWorkedHoursQuery query, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(userId, out var guid) || !await _context.Users.AnyAsync(x => x.Id == guid, cancellationToken))
                throw new InvalidValidationException(nameof(userId), $"'{nameof(userId).ToPascalCase().InsertSpaces()}' is invalid.");

            var fromDate = query.FromDate;
            var toDate = query.ToDate;

            var workedHours = await _context.WorkedHours
                .Include(x => x.Project)
                .Include(x => x.ProjectTask)
                .Where(x => x.UserId == guid && (fromDate.HasValue ? x.Date >= fromDate.Value.Date : true) && (toDate.HasValue ? x.Date <= toDate.Value.Date : true))
                .OrderBy(x => x.Date)
                .ToListAsync(cancellationToken);

            var groupedHours = workedHours
                .GroupBy(x => x.Date.Date)
                .Select(group => new WorkedHourGroupedModel
                {
                    Date = group.Key,
                    WorkedHours = group.Select(hour => new WorkedHourItem
                    {
                        Id = hour.Id,
                        HoursQuantity = hour.HoursQuantity,
                        UserId = hour.UserId.ToString(),
                        ProjectId = hour.ProjectId,
                        ProjectName = hour.Project.Name,
                        ProjectTaskId = hour.ProjectTaskId,
                        ProjectTaskName = hour.ProjectTask.Name
                    }).ToList()
                })
                .OrderBy(x => x.Date)
                .ToList();

            return groupedHours;
        }

        public async Task AddWorkedHourAsync(string userId, AddWorkedHourModel model, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(userId, out var guid) || !await _context.Users.AnyAsync(x => x.Id == guid, cancellationToken))
                throw new InvalidValidationException(nameof(userId), $"'{nameof(userId).ToPascalCase().InsertSpaces()}' is invalid.");

            var project = await _context.Projects.SingleAsync(x => x.Id == model.ProjectId, cancellationToken);
            var projectTask = await _context.ProjectTasks.SingleAsync(x => x.Id == model.ProjectTaskId, cancellationToken);

            if (projectTask.ProjectId != project.Id)
                throw new InvalidOperationException($"Project task with given '{nameof(projectTask.ProjectId).ToPascalCase().InsertSpaces()}' is not assigned to project with given '{nameof(project.Id).ToPascalCase().InsertSpaces()}'.");

            var entity = new WorkedHour
            {
                CreatedDateTime = DateTime.Now,
                Date = model.Date.Date,
                HoursQuantity = model.HoursQuantity,
                ProjectId = project.Id,
                ProjectTaskId = model.ProjectTaskId,
                UserId = guid
            };

            await _context.WorkedHours.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteWorkedHourAsync(string userId, int workedHourId, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(userId, out var guid) || !await _context.Users.AnyAsync(x => x.Id == guid, cancellationToken))
                throw new InvalidValidationException(nameof(userId), $"'{nameof(userId).ToPascalCase().InsertSpaces()}' is invalid.");

            var workedHour = await _context.WorkedHours.SingleAsync(x => x.Id == workedHourId, cancellationToken);

            if (workedHour.UserId != guid)
                throw new InvalidOperationException($"Worked hour with given '{nameof(workedHourId).ToPascalCase().InsertSpaces()}' is not assigned to user with given '{nameof(userId).ToPascalCase().InsertSpaces()}'.");

            _context.WorkedHours.Remove(workedHour);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
