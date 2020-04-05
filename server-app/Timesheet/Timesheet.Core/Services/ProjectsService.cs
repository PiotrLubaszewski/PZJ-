using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Timesheet.Core.Exceptions;
using Timesheet.Core.Extensions;
using Timesheet.Core.Interfaces.Services;
using Timesheet.Core.Models.Collections.Interfaces;
using Timesheet.Core.Models.Helpers;
using Timesheet.Core.Models.Projects;
using Timesheet.Persistence;
using Timesheet.Persistence.Entities;

namespace Timesheet.Core.Services
{
    public class ProjectsService : IProjectsService
    {
        private readonly TimesheetContext _context;

        public ProjectsService(TimesheetContext context)
        {
            _context = context;
        }

        #region Projects
        public async Task<ICollectionResult<ProjectModel>> GetProjectsAsync(OperationQuery operationQuery, CancellationToken cancellationToken)
        {
            return await _context.Projects.ToCollectionResultAsync<Project, ProjectModel>(operationQuery, cancellationToken);
        }

        public async Task<ProjectModel> GetProjectByIdAsync(int projectId, CancellationToken cancellationToken)
        {
            var project = await _context.Projects.SingleOrDefaultAsync(x => x.Id == projectId, cancellationToken);

            if (project == null)
                throw new InvalidOperationException($"Project with given '{nameof(projectId).ToPascalCase().InsertSpaces()}' does not exist.");

            return project.Adapt<ProjectModel>();
        }

        public async Task AddProjectAsync(AddProjectModel model, CancellationToken cancellationToken)
        {
            var project = model.Adapt<Project>();

            await _context.AddAsync(project, cancellationToken);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProjectAsync(UpdateProjectModel model, CancellationToken cancellationToken)
        {
            var project = await _context.Projects.SingleOrDefaultAsync(x => x.Id == model.Id, cancellationToken);

            if (project == null)
                throw new InvalidOperationException($"Project with given '{nameof(model.Id).ToPascalCase().InsertSpaces()}' does not exist.");

            project.Name = model.Name;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteProjectAsync(int projectId, CancellationToken cancellationToken)
        {
            if (projectId < 1)
                throw new InvalidValidationException(nameof(projectId), $"'{nameof(projectId).ToPascalCase().InsertSpaces()}' is invalid.");

            var project = await _context.Projects.SingleOrDefaultAsync(x => x.Id == projectId, cancellationToken);

            if (project == null)
                throw new InvalidOperationException($"Project with given '{nameof(projectId).ToPascalCase().InsertSpaces()}' does not exist.");

            // TODO: Check if there are any hours added with this project

            _context.Remove(project);
            await _context.SaveChangesAsync();
        }
        #endregion

        #region User Projects
        public async Task<ICollectionResult<ProjectModel>> GetUserProjectsAsync(string userId, OperationQuery operationQuery, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(userId, out var guid) || !await _context.Users.AnyAsync(x => x.Id == guid, cancellationToken))
                throw new InvalidValidationException(nameof(userId), $"'{nameof(userId).ToPascalCase().InsertSpaces()}' is invalid.");

            return await _context.Projects.ToCollectionResultAsync<Project, ProjectModel>(operationQuery, cancellationToken);
        }
        #endregion
    }
}
