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
using Timesheet.Core.Models.ProjectTasks;
using Timesheet.Persistence;
using Timesheet.Persistence.Entities;

namespace Timesheet.Core.Services
{
    public class ProjectTasksService : IProjectTasksService
    {
        private readonly TimesheetContext _context;

        public ProjectTasksService(TimesheetContext context)
        {
            _context = context;
        }

        #region User ProjectTasks
        public async Task<ICollectionResult<ProjectTaskModel>> GetUserProjectTasksAsync(string userId, int projectId, OperationQuery operationQuery, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(userId, out var guid) || !await _context.Users.AnyAsync(x => x.Id == guid, cancellationToken))
                throw new InvalidValidationException(nameof(userId), $"'{nameof(userId).ToPascalCase().InsertSpaces()}' is invalid.");

            return await _context.ProjectTasks
                .Where(x => x.UserId == guid && x.ProjectId == projectId)
                .ToCollectionResultAsync<ProjectTask, ProjectTaskModel>(operationQuery, cancellationToken);
        }

        public async Task<ProjectTaskModel> GetUserProjectTaskByIdAsync(string userId, int projectId, int projectTaskId, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(userId, out var guid) || !await _context.Users.AnyAsync(x => x.Id == guid, cancellationToken))
                throw new InvalidValidationException(nameof(userId), $"'{nameof(userId).ToPascalCase().InsertSpaces()}' is invalid.");

            if (!await _context.Projects.AnyAsync(x => x.Id == projectId))
                throw new InvalidOperationException($"Project with given '{nameof(projectId).ToPascalCase().InsertSpaces()}' does not exist.");

            var entity = await _context.ProjectTasks.SingleOrDefaultAsync(x => x.Id == projectTaskId, cancellationToken);
            if (entity == null)
                throw new InvalidOperationException($"Project task with given '{nameof(projectTaskId).ToPascalCase().InsertSpaces()}' does not exist.");

            if (entity.ProjectId != projectId || entity.UserId != guid)
                throw new InvalidOperationException($"Project task is not assigned to given Project or User.");

            return entity.Adapt<ProjectTaskModel>();
        }

        public async Task AddUserProjectTaskAsync(string userId, int projectId, AddProjectTaskModel model, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(userId, out var guid) || !await _context.Users.AnyAsync(x => x.Id == guid, cancellationToken))
                throw new InvalidValidationException(nameof(userId), $"'{nameof(userId).ToPascalCase().InsertSpaces()}' is invalid.");

            if (!await _context.Projects.AnyAsync(x => x.Id == projectId))
                throw new InvalidOperationException($"Project with given '{nameof(projectId).ToPascalCase().InsertSpaces()}' does not exist.");

            var entity = new ProjectTask
            {
                CreatedDateTime = DateTime.Now,
                UserId = guid,
                ProjectId = projectId,
                Name = model.Name
            };

            await _context.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateUserProjectTaskAsync(string userId, int projectId, int projectTaskId, UpdateProjectTaskModel model, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(userId, out var guid) || !await _context.Users.AnyAsync(x => x.Id == guid, cancellationToken))
                throw new InvalidValidationException(nameof(userId), $"'{nameof(userId).ToPascalCase().InsertSpaces()}' is invalid.");

            if (!await _context.Projects.AnyAsync(x => x.Id == projectId))
                throw new InvalidOperationException($"Project with given '{nameof(projectId).ToPascalCase().InsertSpaces()}' does not exist.");

            var entity = await _context.ProjectTasks.SingleOrDefaultAsync(x => x.Id == projectTaskId, cancellationToken);
            if (entity == null)
                throw new InvalidOperationException($"Project task with given '{nameof(projectTaskId).ToPascalCase().InsertSpaces()}' does not exist.");

            if (entity.ProjectId != projectId || entity.UserId != guid)
                throw new InvalidOperationException($"Project task is not assigned to given Project or User.");

            entity.Name = model.Name;

            _context.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteUserProjectTaskAsync(string userId, int projectId, int projectTaskId, CancellationToken cancellationToken) {
            if (!Guid.TryParse(userId, out var guid) || !await _context.Users.AnyAsync(x => x.Id == guid, cancellationToken))
                throw new InvalidValidationException(nameof(userId), $"'{nameof(userId).ToPascalCase().InsertSpaces()}' is invalid.");

            if (!await _context.Projects.AnyAsync(x => x.Id == projectId))
                throw new InvalidOperationException($"Project with given '{nameof(projectId).ToPascalCase().InsertSpaces()}' does not exist.");

            var entity = await _context.ProjectTasks.SingleOrDefaultAsync(x => x.Id == projectTaskId, cancellationToken);
            if (entity == null)
                throw new InvalidOperationException($"Project task with given '{nameof(projectTaskId).ToPascalCase().InsertSpaces()}' does not exist.");

            if (entity.ProjectId != projectId || entity.UserId != guid)
                throw new InvalidOperationException($"Project task is not assigned to given Project or User.");

            _context.ProjectTasks.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

        #endregion
    }
}
