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

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ApiResponse> AddAccountAsync(AddAccountModel model)
        {
            await _accountsService.AddAccountAsync(model);

            return this.Result();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{userId}/roles")]
        public async Task<ApiResponse<ICollectionResult<RoleModel>>> GetUserRolesAsync(string userId, [FromQuery] OperationQuery operationQuery, CancellationToken cancellationToken)
        {
            var result = await _accountsService.GetUserRolesAsync(userId, operationQuery, cancellationToken);

            return this.Result(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("{userId}/roles")]
        public async Task<ApiResponse> AddUserRolesAsync(string userId, AddOrUpdateUserRolesModel model, CancellationToken cancellationToken)
        {
            await _accountsService.AddUserRolesAsync(userId, model, cancellationToken);

            return this.Result();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{userId}/roles")]
        public async Task<ApiResponse> UpdateUserRolesAsync(string userId, AddOrUpdateUserRolesModel model, CancellationToken cancellationToken)
        {
            await _accountsService.UpdateUserRolesAsync(userId, model, cancellationToken);

            return this.Result();
        }

        [AllowAnonymous]
        [HttpPost("authorize")]
        public async Task<ApiResponse<string>> AuthorizeAsync(AuthorizeQuery query)
        {
            var jwtTokenModel = await _accountsService.GetJwtTokenModelAsync(query);
            var token = JwtUtils.CreateJwtTokenFromJwtTokenModel(jwtTokenModel, _jwtSettings);

            return this.Result(token);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("roles")]
        public async Task<ApiResponse<ICollectionResult<RoleModel>>> GetRolesAsync([FromQuery] OperationQuery operationQuery, CancellationToken cancellationToken)
        {
            var result = await _accountsService.GetRolesAsync(operationQuery, cancellationToken);

            return this.Result(result);
        }


    }
}