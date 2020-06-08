using System;
using System.Collections.Generic;
using System.Linq;

namespace Timesheet.Core.Models.Salaries
{
    public class MonthlyTimeConsumingSummaryModel
    {
        public string Fullname { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalConsumedHours => Projects.Sum(x => x.ConsumedHours);

        public IEnumerable<MonthlyTimeConsumingProjectModel> Projects { get; set; }
    }

    public class MonthlyTimeConsumingProjectModel
    {
        public string Name { get; set; }
        public decimal ConsumedHours => Tasks.Sum(x => x.ConsumedHours);

        public IEnumerable<MonthlyTimeConsumingTaskModel> Tasks { get; set; }
    }

    public class MonthlyTimeConsumingTaskModel
    {
        public string Name { get; set; }
        public decimal ConsumedHours { get; set; }
    }
}
