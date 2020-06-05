using System;
using System.Collections.Generic;

namespace Timesheet.Core.Models.WorkedHours
{
    public class WorkedHourGroupedModel
    {
        public DateTime Date { get; set; }
        public IEnumerable<WorkedHourItem> WorkedHours { get; set; }
    }

    public class WorkedHourItem
    {
        public int Id { get; set; }
        public decimal HoursQuantity { get; set; }
        public string UserId { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public int ProjectTaskId { get; set; }
        public string ProjectTaskName { get; set; }
    }
}
