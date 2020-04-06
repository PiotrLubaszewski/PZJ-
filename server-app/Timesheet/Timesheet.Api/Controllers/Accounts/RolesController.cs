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
    }
}
