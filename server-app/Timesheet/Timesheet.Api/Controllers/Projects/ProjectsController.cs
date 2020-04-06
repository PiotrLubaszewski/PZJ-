using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using Timesheet.Api.Infrastructure.Extensions;
using Timesheet.Api.Models;
using Timesheet.Core.Interfaces.Services;
using Timesheet.Core.Models.Collections.Interfaces;
using Timesheet.Core.Models.Helpers;
using Timesheet.Core.Models.Projects;

namespace Timesheet.Api.Controllers.Projects
{
    [ApiController]
    [Route("projects")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectsService _projectsService;

        public ProjectsController(IProjectsService projectsService)
        {
            _projectsService = projectsService;
        }

        /// <summary>
        /// Returns all available projects.
        /// Can be paginated.
        /// Needed role: 'Admin' or 'Manager'.
        /// </summary>
        [Authorize(Roles = "Admin, Manager")]
        [HttpGet]
        public async Task<ApiResponse<ICollectionResult<ProjectModel>>> GetAsync([FromQuery] OperationQuery operationQuery, CancellationToken cancellationToken)
        {
            var result = await _projectsService.GetProjectsAsync(operationQuery, cancellationToken);

            return this.Result(result);
        }

        /// <summary>
        /// Returns specific project.
        /// Needed role: 'Admin' or 'Manager'.
        /// </summary>
        [Authorize(Roles = "Admin, Manager")]
        [HttpGet("{projectId}")]
        public async Task<ApiResponse<ProjectModel>> GetByIdAsync(int projectId, CancellationToken cancellationToken)
        {
            var result = await _projectsService.GetProjectByIdAsync(projectId, cancellationToken);

            return this.Result(result);
        }

        /// <summary>
        /// Adds new project.
        /// Needed role: 'Admin' or 'Manager'.
        /// </summary>
        [Authorize(Roles = "Admin, Manager")]
        [HttpPost]
        public async Task<ApiResponse> PostAsync([FromBody] AddProjectModel model, CancellationToken cancellationToken)
        {
            await _projectsService.AddProjectAsync(model, cancellationToken);

            return this.Result();
        }

        /// <summary>
        /// Updated specific project.
        /// Needed role: 'Admin' or 'Manager'.
        /// </summary>
        [Authorize(Roles = "Admin, Manager")]
        [HttpPut]
        public async Task<ApiResponse> PutAsync([FromBody] UpdateProjectModel model, CancellationToken cancellationToken)
        {
            await _projectsService.UpdateProjectAsync(model, cancellationToken);

            return this.Result();
        }

        /// <summary>
        /// Deletes specific project.
        /// Needed role: 'Admin' or 'Manager'.
        /// </summary>
        [Authorize(Roles = "Admin, Manager")]
        [HttpDelete("{projectId}")]
        public async Task<ApiResponse> DeleteAsync(int projectId, CancellationToken cancellationToken)
        {
            await _projectsService.DeleteProjectAsync(projectId, cancellationToken);

            return this.Result();
        }
    }
}
