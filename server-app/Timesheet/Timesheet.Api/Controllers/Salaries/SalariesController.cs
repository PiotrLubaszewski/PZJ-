using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using Timesheet.Api.Infrastructure.Extensions;
using Timesheet.Api.Models;
using Timesheet.Core.Interfaces.Services;
using Timesheet.Core.Models.Collections.Interfaces;
using Timesheet.Core.Models.Helpers;
using Timesheet.Core.Models.Salaries;

namespace Timesheet.Api.Controllers.Salaries
{
    [ApiController]
    [Route("salaries")]
    public class SalariesController : ControllerBase
    {
        private readonly ISalariesService _salariesService;

        public SalariesController(ISalariesService salariesService)
        {
            _salariesService = salariesService;
        }

        /// <summary>
        /// Returns all salaries assigned to specific account.
        /// Can be paginated.
        /// Needed role: 'Admin' or 'Manager'.
        /// </summary>
        //[Authorize(Roles = "Admin, Manager")]
        [HttpGet("/accounts/{userId}/salaries")]
        public async Task<ApiResponse<ICollectionResult<SalaryModel>>> GetUserSalariesAsync(string userId, [FromQuery] OperationQuery operationQuery, CancellationToken cancellationToken)
        {
            var result = await _salariesService.GetUserSalariesAsync(userId, operationQuery, cancellationToken);

            return this.Result(result);
        }

        /// <summary>
        /// Returns specific salary assigned to specific account.
        /// Needed role: 'Admin' or 'Manager'.
        /// </summary>
        //[Authorize(Roles = "Admin, Manager")]
        [HttpGet("/accounts/{userId}/salaries/{salaryId}")]
        public async Task<ApiResponse<SalaryModel>> GetUserSalaryByIdAsync(string userId, int salaryId, CancellationToken cancellationToken)
        {
            var result = await _salariesService.GetUserSalaryByIdAsync(userId, salaryId, cancellationToken);

            return this.Result(result);
        }

        /// <summary>
        /// Returns current salary assigned to specific account.
        /// Needed role: 'Admin' or 'Manager'.
        /// </summary>
        //[Authorize(Roles = "Admin, Manager")]
        [HttpGet("/accounts/{userId}/salaries/current")]
        public async Task<ApiResponse<SalaryModel>> GetCurrentUserSalaryAsync(string userId, CancellationToken cancellationToken)
        {
            var result = await _salariesService.GetCurrentUserSalaryAsync(userId, cancellationToken);

            return this.Result(result);
        }

        /// <summary>
        /// Returns last salary assigned to specific account.
        /// Needed role: 'Admin' or 'Manager'.
        /// </summary>
        //[Authorize(Roles = "Admin, Manager")]
        [HttpGet("/accounts/{userId}/salaries/last")]
        public async Task<ApiResponse<SalaryModel>> GetLastUserSalaryAsync(string userId, CancellationToken cancellationToken)
        {
            var result = await _salariesService.GetLastUserSalaryAsync(userId, cancellationToken);

            return this.Result(result);
        }

        /// <summary>
        /// Adds new salary to specific account.
        /// Needed role: 'Admin' or 'Manager'.
        /// </summary>
        //[Authorize(Roles = "Admin, Manager")]
        [HttpPost("/accounts/{userId}/salaries")]
        public async Task<ApiResponse> AddSalaryAsync(string userId, AddSalaryModel model, CancellationToken cancellationToken)
        {
            await _salariesService.AddSalaryAsync(userId, model, cancellationToken);

            return this.Result();
        }

        /// <summary>
        /// Updates specific salary assigned to specific account.
        /// Needed role: 'Admin' or 'Manager'.
        /// </summary>
        //[Authorize(Roles = "Admin, Manager")]
        [HttpPut("/accounts/{userId}/salaries")]
        public async Task<ApiResponse> UpdateSalaryModelAsync(string userId, UpdateSalaryModel model, CancellationToken cancellationToken)
        {
            await _salariesService.UpdateSalaryModelAsync(userId, model, cancellationToken);

            return this.Result();
        }

        /// <summary>
        /// Deletes specific salary assigned to specific account.
        /// Needed role: 'Admin' or 'Manager'.
        /// </summary>
        //[Authorize(Roles = "Admin, Manager")]
        [HttpDelete("/accounts/{userId}/salaries/{salaryId}")]
        public async Task<ApiResponse> DeleteSalaryAsync(string userId, int salaryId, CancellationToken cancellationToken)
        {
            await _salariesService.DeleteSalaryAsync(userId, salaryId, cancellationToken);

            return this.Result();
        }

        /// <summary>
        /// Returns calculated salary for given month of a year.
        /// Needed role: 'Admin' or 'Manager'.
        /// </summary>
        //[Authorize(Roles = "Admin, Manager")]
        [HttpGet("/accounts/{userId}/month-salaries/{year}/{month}")]
        public async Task<ApiResponse<MonthSalaryModel>> GetMonthSalaryAsync(string userId, int year, int month, CancellationToken cancellationToken)
        {
            var result = await _salariesService.GetMonthSalaryAsync(userId, year, month, cancellationToken);

            return this.Result(result);
        }
    }
}
