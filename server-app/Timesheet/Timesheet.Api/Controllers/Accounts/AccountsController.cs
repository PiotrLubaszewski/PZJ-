namespace Timesheet.Api.Controllers.Accounts
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading;
    using System.Threading.Tasks;
    using Timesheet.Api.Infrastructure.Extensions;
    using Timesheet.Api.Infrastructure.Utils;
    using Timesheet.Api.Models;
    using Timesheet.Api.Models.Settings;
    using Timesheet.Core.Interfaces.Services;
    using Timesheet.Core.Models.Accounts;
    using Timesheet.Core.Models.Collections.Interfaces;
    using Timesheet.Core.Models.Helpers;
    using Timesheet.Core.Models.Queries.Accounts;

    [ApiController]
    [Route("accounts")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountsService _accountsService;
        private readonly JwtSettings _jwtSettings;

        public AccountsController(IAccountsService accountsService, JwtSettings jwtSettings)
        {
            _accountsService = accountsService;
            _jwtSettings = jwtSettings;
        }

        #region Authorization
        /// <summary>
        /// Authorize an account using given credentials and returns JWT.
        /// </summary>
        [AllowAnonymous]
        [HttpPost("authorize")]
        public async Task<ApiResponse<string>> AuthorizeAsync(AuthorizeQuery query)
        {
            var jwtTokenModel = await _accountsService.GetJwtTokenModelAsync(query);
            var token = JwtUtils.CreateJwtTokenFromJwtTokenModel(jwtTokenModel, _jwtSettings);

            return this.Result(token);
        }
        #endregion

        #region Accounts management
        /// <summary>
        /// Returns all available accounts.
        /// Can be paginated.
        /// Needed role: 'Admin' or 'Manager'.
        /// </summary>
        [Authorize(Roles = "Admin, Manager")]
        [HttpGet]
        public async Task<ApiResponse<ICollectionResult<AccountModel>>> GetUsersAsync([FromQuery] OperationQuery operationQuery, CancellationToken cancellationToken)
        {
            var result = await _accountsService.GetUsersAsync(operationQuery, cancellationToken);

            return this.Result(result);
        }

        /// <summary>
        /// Returns specific account.
        /// Needed role: 'Admin' or 'Manager'.
        /// </summary>
        [Authorize(Roles = "Admin, Manager")]
        [HttpGet("{userId}")]
        public async Task<ApiResponse<AccountModel>> GetUserByIdAsync(string userId)
        {
            var result = await _accountsService.GetUserByIdAsync(userId);

            return this.Result(result);
        }

        /// <summary>
        /// Adds new account.
        /// Needed role: 'Admin'.
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ApiResponse> AddAccountAsync(AddAccountModel model)
        {
            await _accountsService.AddUserAsync(model);

            return this.Result();
        }
        #endregion
    }
}