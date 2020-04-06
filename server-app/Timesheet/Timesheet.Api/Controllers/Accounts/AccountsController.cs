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
    using Timesheet.Core.Models.Projects;
    using Timesheet.Core.Models.Queries.Accounts;
    using Timesheet.Core.Models.Salaries;

    [ApiController]
    [Route("accounts")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountsService _accountsService;
        private readonly ISalariesService _salariesService;
        private readonly IProjectsService _projectsService;
        private readonly JwtSettings _jwtSettings;

        public AccountsController(IAccountsService accountsService, ISalariesService salariesService, IProjectsService projectsService, JwtSettings jwtSettings)
        {
            _accountsService = accountsService;
            _salariesService = salariesService;
            _projectsService = projectsService;
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

        #region Roles
        /// <summary>
        /// Returns all roles assigned to specific account.
        /// Can be paginated.
        /// Needed role: 'Admin'.
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpGet("{userId}/roles")]
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
        [HttpPost("{userId}/roles")]
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
        [HttpPut("{userId}/roles")]
        public async Task<ApiResponse> UpdateUserRolesAsync(string userId, AddOrUpdateUserRolesModel model, CancellationToken cancellationToken)
        {
            await _accountsService.UpdateUserRolesAsync(userId, model, cancellationToken);

            return this.Result();
        }
        #endregion

        #region Salaries
        /// <summary>
        /// Returns all salaries assigned to specific account.
        /// Can be paginated.
        /// Needed role: 'Admin' or 'Manager'.
        /// </summary>
        [Authorize(Roles = "Admin, Manager")]
        [HttpGet("{userId}/salaries")]
        public async Task<ApiResponse<ICollectionResult<SalaryModel>>> GetUserSalariesAsync(string userId, [FromQuery] OperationQuery operationQuery, CancellationToken cancellationToken)
        {
            var result = await _salariesService.GetUserSalariesAsync(userId, operationQuery, cancellationToken);

            return this.Result(result);
        }

        /// <summary>
        /// Returns specific salary assigned to specific account.
        /// Needed role: 'Admin' or 'Manager'.
        /// </summary>
        [Authorize(Roles = "Admin, Manager")]
        [HttpGet("{userId}/salaries/{salaryId}")]
        public async Task<ApiResponse<SalaryModel>> GetUserSalaryByIdAsync(string userId, int salaryId, CancellationToken cancellationToken)
        {
            var result = await _salariesService.GetUserSalaryByIdAsync(userId, salaryId, cancellationToken);

            return this.Result(result);
        }

        /// <summary>
        /// Returns current salary assigned to specific account.
        /// Needed role: 'Admin' or 'Manager'.
        /// </summary>
        [Authorize(Roles = "Admin, Manager")]
        [HttpGet("{userId}/salaries/current")]
        public async Task<ApiResponse<SalaryModel>> GetCurrentUserSalaryAsync(string userId, CancellationToken cancellationToken)
        {
            var result = await _salariesService.GetCurrentUserSalaryAsync(userId, cancellationToken);

            return this.Result(result);
        }

        /// <summary>
        /// Returns last salary assigned to specific account.
        /// Needed role: 'Admin' or 'Manager'.
        /// </summary>
        [Authorize(Roles = "Admin, Manager")]
        [HttpGet("{userId}/salaries/last")]
        public async Task<ApiResponse<SalaryModel>> GetLastUserSalaryAsync(string userId, CancellationToken cancellationToken)
        {
            var result = await _salariesService.GetLastUserSalaryAsync(userId, cancellationToken);

            return this.Result(result);
        }

        /// <summary>
        /// Adds new salary to specific account.
        /// Needed role: 'Admin' or 'Manager'.
        /// </summary>
        [Authorize(Roles = "Admin, Manager")]
        [HttpPost("{userId}/salaries")]
        public async Task<ApiResponse> AddSalaryAsync(string userId, AddSalaryModel model, CancellationToken cancellationToken)
        {
            await _salariesService.AddSalaryAsync(userId, model, cancellationToken);

            return this.Result();
        }

        /// <summary>
        /// Updates specific salary assigned to specific account.
        /// Needed role: 'Admin' or 'Manager'.
        /// </summary>
        [Authorize(Roles = "Admin, Manager")]
        [HttpPut("{userId}/salaries")]
        public async Task<ApiResponse> UpdateSalaryModelAsync(string userId, UpdateSalaryModel model, CancellationToken cancellationToken)
        {
            await _salariesService.UpdateSalaryModelAsync(userId, model, cancellationToken);

            return this.Result();
        }

        /// <summary>
        /// Deletes specific salary assigned to specific account.
        /// Needed role: 'Admin' or 'Manager'.
        /// </summary>
        [Authorize(Roles = "Admin, Manager")]
        [HttpDelete("{userId}/salaries/{salaryId}")]
        public async Task<ApiResponse> DeleteSalaryAsync(string userId, int salaryId, CancellationToken cancellationToken)
        {
            await _salariesService.DeleteSalaryAsync(userId, salaryId, cancellationToken);

            return this.Result();
        }
        #endregion

        #region Projects
        /// <summary>
        /// Returns all projects assigned to specific account.
        /// Can be paginated.
        /// Needed role: 'Admin' or 'Manager'.
        /// </summary>
        [Authorize(Roles = "Admin, Manager")]
        [HttpGet("{userId}/projects")]
        public async Task<ApiResponse<ICollectionResult<ProjectModel>>> GetAsync(string userId, [FromQuery] OperationQuery operationQuery, CancellationToken cancellationToken)
        {
            var result = await _projectsService.GetUserProjectsAsync(userId, operationQuery, cancellationToken);

            return this.Result(result);
        }

        /// <summary>
        /// Adds new project to specific account.
        /// Needed role: 'Admin' or 'Manager'.
        /// </summary>
        [Authorize(Roles = "Admin, Manager")]
        [HttpPost("{userId}/projects")]
        public async Task<ApiResponse> PostAsync(string userId, [FromBody] AddUserProjectModel model, CancellationToken cancellationToken)
        {
            await _projectsService.AddUserProjectAsync(userId, model, cancellationToken);

            return this.Result();
        }

        /// <summary>
        /// Deletes specific project assigned to specific account.
        /// Needed role: 'Admin' or 'Manager'.
        /// </summary>
        [Authorize(Roles = "Admin, Manager")]
        [HttpDelete("{userId}/projects/{projectId}")]
        public async Task<ApiResponse> DeleteAsync(string userId, int projectId, CancellationToken cancellationToken)
        {
            await _projectsService.DeleteUserProjectAsync(userId, projectId, cancellationToken);

            return this.Result();
        }
        #endregion
    }
}