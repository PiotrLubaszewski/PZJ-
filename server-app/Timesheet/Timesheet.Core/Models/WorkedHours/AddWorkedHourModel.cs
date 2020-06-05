using System;

namespace Timesheet.Core.Models.WorkedHours
{
    public class AddWorkedHourModel
    {
        public int ProjectId { get; set; }
        public int ProjectTaskId { get; set; }
        public DateTime Date { get; set; }
        public int HoursQuantity { get; set; }
    }
}
