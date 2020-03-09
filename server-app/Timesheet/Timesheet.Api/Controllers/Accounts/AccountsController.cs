namespace Timesheet.Api.Controllers.Accounts
{
    using Microsoft.AspNetCore.Mvc;
    using Timesheet.Api.Models;
    using Timesheet.Core.Models.Queries.Accounts;

    [ApiController]
    [Route("accounts")]
    public class AccountsController : Controller
    {
        [HttpPost("authorize")]
        public ApiResponse<string> AuthorizeAsync(AuthorizeQuery query)
        {
            return new ApiResponse<string>
            {
                Result = "test",
                StatusCode = 200
            };
        }
    }
}