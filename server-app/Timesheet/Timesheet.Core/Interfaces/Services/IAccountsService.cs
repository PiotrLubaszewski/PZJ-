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
        // Authorization
        Task<JwtTokenModel> GetJwtTokenModelAsync(AuthorizeQuery model);

        // Roles
        Task<ICollectionResult<RoleModel>> GetRolesAsync(OperationQuery operationQuery, CancellationToken cancellationToken);

        // Users
        Task<ICollectionResult<AccountModel>> GetUsersAsync(OperationQuery operationQuery, CancellationToken cancellationToken);
        Task<AccountModel> GetUserByIdAsync(string userId);
        Task AddUserAsync(AddAccountModel model);

        // User Roles
        Task<ICollectionResult<RoleModel>> GetUserRolesAsync(string userId, OperationQuery operationQuery, CancellationToken cancellationToken);
        Task AddUserRolesAsync(string userId, AddOrUpdateUserRolesModel model, CancellationToken cancellationToken);
        Task UpdateUserRolesAsync(string userId, AddOrUpdateUserRolesModel model, CancellationToken cancellationToken);
    }
}
