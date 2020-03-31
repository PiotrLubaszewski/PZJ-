using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Timesheet.Core.Models.Accounts;
using Timesheet.Core.Models.Collections.Interfaces;
using Timesheet.Core.Models.Helpers;
using Timesheet.Core.Models.Queries.Accounts;

namespace Timesheet.Core.Interfaces.Services
{
    public interface IAccountsService
    {
        Task<JwtTokenModel> GetJwtTokenModelAsync(AuthorizeQuery model);
        Task<ICollectionResult<AccountModel>> GetUsersAsync(OperationQuery operationQuery, CancellationToken cancellationToken);
        Task<ICollectionResult<RoleModel>> GetRolesAsync(OperationQuery operationQuery, CancellationToken cancellationToken);
        Task<AccountModel> GetUserByIdAsync(string userId);
        Task<ICollectionResult<RoleModel>> GetUserRolesAsync(string userId, OperationQuery operationQuery, CancellationToken cancellationToken);
        Task AddAccountAsync(AddAccountModel model);
        Task AddUserRolesAsync(string userId, AddOrUpdateUserRolesModel model, CancellationToken cancellationToken);
        Task UpdateUserRolesAsync(string userId, AddOrUpdateUserRolesModel model, CancellationToken cancellationToken);
    }
}
