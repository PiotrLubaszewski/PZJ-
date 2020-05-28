using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using Timesheet.Api.Infrastructure.Extensions;
using Timesheet.Api.Models;
using Timesheet.Core.Interfaces.Services;
using Timesheet.Core.Models.Accounts;
using Timesheet.Core.Models.Collections.Interfaces;
using Timesheet.Core.Models.Helpers;

namespace Timesheet.Api.Controllers.Accounts
{
    [ApiController]
    [Route("roles")]
    public class RolesController : ControllerBase
    {
        private readonly IAccountsService _accountsService;

        public RolesController(IAccountsService accountsService)
        {
            _accountsService = accountsService;
        }

        /// <summary>
        /// Returns all available roles.
        /// Can be paginated.
        /// Needed role: 'Admin'.
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ApiResponse<ICollectionResult<RoleModel>>> GetRolesAsync([FromQuery] OperationQuery operationQuery, CancellationToken cancellationToken)
        {
            var result = await _accountsService.GetRolesAsync(operationQuery, cancellationToken);

            return this.Result(result);
        }

        /// <summary>
        /// Returns all roles assigned to specific account.
        /// Can be paginated.
        /// Needed role: 'Admin'.
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpGet("/accounts/{userId}/roles")]
        public async Task<ApiResponse<ICollectionResult<RoleModel>>> GetUserRolesAsync(string userId, [FromQuery] OperationQuery operationQuery, CancellationToken cancellationToken)
        {
            var result = await _accountsService.GetUserRolesAsync(userId, operationQuery, cancellationToken);

            return this.Result(result);
        }

        /// <summary>
        /// Assign new roles to specific account.
        /// Needed role: 'Admin'.
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpPost("/accounts/{userId}/roles")]
        public async Task<ApiResponse> AddUserRolesAsync(string userId, AddOrUpdateUserRolesModel model, CancellationToken cancellationToken)
        {
            await _accountsService.AddUserRolesAsync(userId, model, cancellationToken);

            return this.Result();
        }

        /// <summary>
        /// Updates roles assigned to specific account.
        /// Needed role: 'Admin'.
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpPut("/accounts/{userId}/roles")]
        public async Task<ApiResponse> UpdateUserRolesAsync(string userId, AddOrUpdateUserRolesModel model, CancellationToken cancellationToken)
        {
            await _accountsService.UpdateUserRolesAsync(userId, model, cancellationToken);

            return this.Result();
        }
    }
}
