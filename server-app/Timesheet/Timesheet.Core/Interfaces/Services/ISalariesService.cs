using System.Threading;
using System.Threading.Tasks;
using Timesheet.Core.Models.Collections.Interfaces;
using Timesheet.Core.Models.Helpers;
using Timesheet.Core.Models.Salaries;

namespace Timesheet.Core.Interfaces.Services
{
    public interface ISalariesService
    {
        Task<ICollectionResult<SalaryModel>> GetUserSalariesAsync(string userId, OperationQuery operationQuery, CancellationToken cancellationToken);
        Task<SalaryModel> GetUserSalaryByIdAsync(string userId, int salaryId, CancellationToken cancellationToken);
        Task<SalaryModel> GetCurrentUserSalaryAsync(string userId, CancellationToken cancellationToken);
        Task<SalaryModel> GetLastUserSalaryAsync(string userId, CancellationToken cancellationToken);
        Task AddSalaryAsync(string userId, AddSalaryModel model, CancellationToken cancellationToken);
        Task UpdateSalaryModelAsync(string userId, UpdateSalaryModel model, CancellationToken cancellationToken);
        Task DeleteSalaryAsync(string userId, int salaryId, CancellationToken cancellationToken);
    }
}
