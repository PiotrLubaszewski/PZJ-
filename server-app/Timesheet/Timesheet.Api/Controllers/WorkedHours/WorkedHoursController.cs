using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Timesheet.Api.Infrastructure.Extensions;
using Timesheet.Api.Models;
using Timesheet.Core.Interfaces.Services;
using Timesheet.Core.Models.WorkedHours;

namespace Timesheet.Api.Controllers.ProjectTasks
{
    [ApiController]
    [Route("worked-hours")]
    public class WorkedHoursController : ControllerBase
    {
        private readonly IWorkedHoursService _workedHoursService;

        public WorkedHoursController(IWorkedHoursService workedHoursService)
        {
            _workedHoursService = workedHoursService;
        }

        /// <summary>
        /// Returns accounts worked hours grouped by day.
        /// Needed role: 'Admin' or 'Manager'.
        /// </summary>
        //[Authorize(Roles = "Admin, Manager")]
        [HttpGet("/accounts/{userId}/worked-hours")]
        public async Task<ApiResponse<IEnumerable<WorkedHourGroupedModel>>> GetWorkedHoursAsync(string userId, [FromQuery] GetWorkedHoursQuery query, CancellationToken cancellationToken)
        {
            var result = await _workedHoursService.GetWorkedHoursAsync(userId, query, cancellationToken);

            return this.Result(result);
        }

        /// <summary>
        /// Adds new worked hours to specific account.
        /// Needed role: 'Admin' or 'Manager'.
        /// </summary>
        //[Authorize(Roles = "Admin, Manager")]
        [HttpPost("/accounts/{userId}/worked-hours")]
        public async Task<ApiResponse> AddWorkedHourAsync(string userId, [FromBody] AddWorkedHourModel model, CancellationToken cancellationToken)
        {
            await _workedHoursService.AddWorkedHourAsync(userId, model, cancellationToken);

            return this.Result();
        }

        /// <summary>
        /// Deletes worked hours from specific account.
        /// Needed role: 'Admin' or 'Manager'.
        /// </summary>
        //[Authorize(Roles = "Admin, Manager")]
        [HttpDelete("/accounts/{userId}/worked-hours/{workedHourId}")]
        public async Task<ApiResponse> DeleteWorkedHourAsync(string userId, int workedHourId, CancellationToken cancellationToken)
        {
            await _workedHoursService.DeleteWorkedHourAsync(userId, workedHourId, cancellationToken);

            return this.Result();
        }
    }
}
