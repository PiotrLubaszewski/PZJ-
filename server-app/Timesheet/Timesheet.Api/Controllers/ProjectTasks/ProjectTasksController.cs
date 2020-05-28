using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using Timesheet.Api.Infrastructure.Extensions;
using Timesheet.Api.Models;
using Timesheet.Core.Interfaces.Services;
using Timesheet.Core.Models.Collections.Interfaces;
using Timesheet.Core.Models.Helpers;
using Timesheet.Core.Models.ProjectTasks;

namespace Timesheet.Api.Controllers.ProjectTasks
{
    [ApiController]
    [Route("project-tasks")]
    public class ProjectTasksController : ControllerBase
    {
        private readonly IProjectTasksService _projectTasksService;

        public ProjectTasksController(IProjectTasksService projectTasksService)
        {
            _projectTasksService = projectTasksService;
        }

        /// <summary>
        /// Returns collection of project task of specific accounts project.
        /// Needed role: 'Admin' or 'Manager'.
        /// </summary>
        [Authorize(Roles = "Admin, Manager")]
        [HttpGet("/accounts/{userId}/projects/{projectId}/tasks")]
        public async Task<ApiResponse<ICollectionResult<ProjectTaskModel>>> GetUserProjectTasksAsync(string userId, int projectId, [FromQuery] OperationQuery operationQuery, CancellationToken cancellationToken)
        {
            var result = await _projectTasksService.GetUserProjectTasksAsync(userId, projectId, operationQuery, cancellationToken);
            
            return this.Result(result);
        }

        /// <summary>
        /// Rreturns specific project task of specific accounts project.
        /// Needed role: 'Admin' or 'Manager'.
        /// </summary>
        [Authorize(Roles = "Admin, Manager")]
        [HttpGet("/accounts/{userId}/projects/{projectId}/tasks/{projectTaskId}")]
        public async Task<ApiResponse<ProjectTaskModel>> GetUserProjectTaskByIdAsync(string userId, int projectId, int projectTaskId, CancellationToken cancellationToken)
        {
            var result = await _projectTasksService.GetUserProjectTaskByIdAsync(userId, projectId, projectTaskId, cancellationToken);

            return this.Result(result);
        }

        /// <summary>
        /// Adds new task to specific accounts project.
        /// Needed role: 'Admin' or 'Manager'.
        /// </summary>
        [Authorize(Roles = "Admin, Manager")]
        [HttpPost("/accounts/{userId}/projects/{projectId}/tasks")]
        public async Task<ApiResponse> AddUserProjectTaskAsync(string userId, int projectId, [FromBody] AddProjectTaskModel model, CancellationToken cancellationToken)
        {
            await _projectTasksService.AddUserProjectTaskAsync(userId, projectId, model, cancellationToken);

            return this.Result();
        }

        /// <summary>
        /// Updates specific task from specific accounts project.
        /// Needed role: 'Admin' or 'Manager'.
        /// </summary>
        [Authorize(Roles = "Admin, Manager")]
        [HttpPut("/accounts/{userId}/projects/{projectId}/tasks/{projectTaskId}")]
        public async Task<ApiResponse> UpdateUserProjectTaskAsync(string userId, int projectId, int projectTaskId, [FromBody] UpdateProjectTaskModel model, CancellationToken cancellationToken)
        {
            await _projectTasksService.UpdateUserProjectTaskAsync(userId, projectId, projectTaskId, model, cancellationToken);

            return this.Result();
        }

        /// <summary>
        /// Deletes specific task from specific accounts project.
        /// Needed role: 'Admin' or 'Manager'.
        /// </summary>
        [Authorize(Roles = "Admin, Manager")]
        [HttpDelete("/accounts/{userId}/projects/{projectId}/tasks/{projectTaskId}")]
        public async Task<ApiResponse> DeleteUserProjectTaskAsync(string userId, int projectId, int projectTaskId, CancellationToken cancellationToken)
        {
            await _projectTasksService.DeleteUserProjectTaskAsync(userId, projectId, projectTaskId, cancellationToken);

            return this.Result();
        }
    }
}
