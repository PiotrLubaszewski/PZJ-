using System.Threading;
using System.Threading.Tasks;
using Timesheet.Core.Models.Collections.Interfaces;
using Timesheet.Core.Models.Helpers;
using Timesheet.Core.Models.ProjectTasks;

namespace Timesheet.Core.Interfaces.Services
{
    public interface IProjectTasksService
    {
        Task<ICollectionResult<ProjectTaskModel>> GetUserProjectTasksAsync(string userId, int projectId, OperationQuery operationQuery, CancellationToken cancellationToken);
        Task<ProjectTaskModel> GetUserProjectTaskByIdAsync(string userId, int projectId, int projectTaskId, CancellationToken cancellationToken);
        Task AddUserProjectTaskAsync(string userId, int projectId, AddProjectTaskModel model, CancellationToken cancellationToken);
        Task UpdateUserProjectTaskAsync(string userId, int projectId, int projectTaskId, UpdateProjectTaskModel model, CancellationToken cancellationToken);
        Task DeleteUserProjectTaskAsync(string userId, int projectId, int projectTaskId, CancellationToken cancellationToken);
    }
}
