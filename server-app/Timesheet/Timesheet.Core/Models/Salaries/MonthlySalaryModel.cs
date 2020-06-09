using System;
using System.Collections.Generic;
using System.Linq;

namespace Timesheet.Core.Models.Salaries
{
    public class MonthlySalaryModel
    {
        public DateTime Date { get; set; }
        public IEnumerable<MonthlyDaySalaryModel> Days { get; set; }
        public decimal SalaryAmount { get; set; }
        public string Fullname { get; set; }

        public decimal TotalFullTimeHours => Days.Sum(x => x.FullTimeHours);
        public decimal TotalPredictedHours => Days.Sum(x => x.PredictedHours);
        public decimal TotalWorkedHours => Days.Sum(x => x.WorkedHours);
        public decimal TotalWorkedHoursRatio => decimal.Round(TotalWorkedHours / TotalFullTimeHours, 2, MidpointRounding.AwayFromZero);
        public int TotalDays => Days.Count();
        public int WorkingDays => Days.Count(x => x.PredictedHours > 0);
    }

    public class MonthlyDaySalaryModel
    {
        public DateTime Date { get; set; }
        public decimal FullTimeHours { get; set; }
        public decimal PredictedHours { get; set; }
        public decimal WorkedHours { get; set; }
    }
}
