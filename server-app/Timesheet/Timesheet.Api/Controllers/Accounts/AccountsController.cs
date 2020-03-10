namespace Timesheet.Api.Controllers.Accounts
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using Timesheet.Api.Infrastructure.Extensions;
    using Timesheet.Api.Infrastructure.Utils;
    using Timesheet.Api.Models;
    using Timesheet.Api.Models.Settings;
    using Timesheet.Core.Interfaces.Services;
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


        [AllowAnonymous]
        [HttpPost("authorize")]
        public async Task<ApiResponse<string>> AuthorizeAsync(AuthorizeQuery query)
        {
            var jwtTokenModel = await _accountsService.GetJwtTokenModelAsync(query);
            var token = JwtUtils.CreateJwtTokenFromJwtTokenModel(jwtTokenModel, _jwtSettings);

            return this.Result(token);
        }
    }
}