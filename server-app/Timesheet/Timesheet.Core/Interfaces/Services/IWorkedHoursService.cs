using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Timesheet.Core.Models.WorkedHours;

namespace Timesheet.Core.Interfaces.Services
{
    public interface IWorkedHoursService
    {
        Task<IEnumerable<WorkedHourGroupedModel>> GetWorkedHoursAsync(string userId, GetWorkedHoursQuery query, CancellationToken cancellationToken);
        Task AddWorkedHourAsync(string userId, AddWorkedHourModel model, CancellationToken cancellationToken);
        Task DeleteWorkedHourAsync(string userId, int workedHourId, CancellationToken cancellationToken);
    }
}
