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
    using Timesheet.Core.Models.Salaries;

    [ApiController]
    [Route("accounts")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountsService _accountsService;
        private readonly ISalariesService _salariesService;
        private readonly JwtSettings _jwtSettings;

        public AccountsController(IAccountsService accountsService, ISalariesService salariesService, JwtSettings jwtSettings)
        {
            _accountsService = accountsService;
            _salariesService = salariesService;
            _jwtSettings = jwtSettings;
        }

        #region Accounts management

        [Authorize(Roles = "Admin, Manager")]
        [HttpGet]
        public async Task<ApiResponse<ICollectionResult<AccountModel>>> GetUsersAsync([FromQuery] OperationQuery operationQuery, CancellationToken cancellationToken)
        {
            var result = await _accountsService.GetUsersAsync(operationQuery, cancellationToken);

            return this.Result(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ApiResponse> AddAccountAsync(AddAccountModel model)
        {
            await _accountsService.AddAccountAsync(model);

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

        [Authorize(Roles = "Admin, Manager")]
        [HttpGet("{userId}")]
        public async Task<ApiResponse<AccountModel>> GetUserByIdAsync(string userId)
        {
            var result = await _accountsService.GetUserByIdAsync(userId);

            return this.Result(result);
        }

        #endregion

        #region Roles

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

        #endregion

        #region Salaries

        [Authorize(Roles = "Admin, Manager")]
        [HttpPost("{userId}/salaries")]
        public async Task<ApiResponse> AddSalaryAsync(string userId, AddSalaryModel model, CancellationToken cancellationToken)
        {
            await _salariesService.AddSalaryAsync(userId, model, cancellationToken);

            return this.Result();
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpPut("{userId}/salaries")]
        public async Task<ApiResponse> UpdateSalaryModelAsync(string userId, UpdateSalaryModel model, CancellationToken cancellationToken)
        {
            await _salariesService.UpdateSalaryModelAsync(userId, model, cancellationToken);

            return this.Result();
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpGet("{userId}/salaries")]
        public async Task<ApiResponse<ICollectionResult<SalaryModel>>> GetUserSalariesAsync(string userId, [FromQuery] OperationQuery operationQuery, CancellationToken cancellationToken)
        {
            var result = await _salariesService.GetUserSalariesAsync(userId, operationQuery, cancellationToken);

            return this.Result(result);
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpGet("{userId}/salaries/{salaryId}")]
        public async Task<ApiResponse<SalaryModel>> GetUserSalaryByIdAsync(string userId, int salaryId, CancellationToken cancellationToken)
        {
            var result = await _salariesService.GetUserSalaryByIdAsync(userId, salaryId, cancellationToken);

            return this.Result(result);
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpGet("{userId}/salaries/current")]
        public async Task<ApiResponse<SalaryModel>> GetCurrentUserSalaryAsync(string userId, CancellationToken cancellationToken)
        {
            var result = await _salariesService.GetCurrentUserSalaryAsync(userId, cancellationToken);

            return this.Result(result);
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpGet("{userId}/salaries/last")]
        public async Task<ApiResponse<SalaryModel>> GetLastUserSalaryAsync(string userId, CancellationToken cancellationToken)
        {
            var result = await _salariesService.GetLastUserSalaryAsync(userId, cancellationToken);

            return this.Result(result);
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpDelete("{userId}/salaries/{salaryId}")]
        public async Task<ApiResponse> DeleteSalaryAsync(string userId, int salaryId, CancellationToken cancellationToken)
        {
            await _salariesService.DeleteSalaryAsync(userId, salaryId, cancellationToken);

            return this.Result();
        }
        #endregion
    }
}