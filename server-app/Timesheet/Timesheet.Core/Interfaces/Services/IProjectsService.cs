using System.Threading;
using System.Threading.Tasks;
using Timesheet.Core.Models.Collections.Interfaces;
using Timesheet.Core.Models.Helpers;
using Timesheet.Core.Models.Projects;

namespace Timesheet.Core.Interfaces.Services
{
    public interface IProjectsService
    {
        // Projects
        Task<ICollectionResult<ProjectModel>> GetProjectsAsync(OperationQuery operationQuery, CancellationToken cancellationToken);
        Task<ProjectModel> GetProjectByIdAsync(int projectId, CancellationToken cancellationToken);
        Task AddProjectAsync(AddProjectModel model, CancellationToken cancellationToken);
        Task UpdateProjectAsync(UpdateProjectModel model, CancellationToken cancellationToken);
        Task DeleteProjectAsync(int projectId, CancellationToken cancellationToken);

        // User Projects
        Task<ICollectionResult<ProjectModel>> GetUserProjectsAsync(string userId, OperationQuery operationQuery, CancellationToken cancellationToken);
    }
}
