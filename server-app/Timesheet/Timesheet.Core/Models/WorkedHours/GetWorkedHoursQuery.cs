using System;

namespace Timesheet.Core.Models.WorkedHours
{
    public class GetWorkedHoursQuery
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
