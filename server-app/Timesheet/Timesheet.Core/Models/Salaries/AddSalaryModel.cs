using System;

namespace Timesheet.Core.Models.Salaries
{
    public class AddSalaryModel
    {
        public DateTime StartDateTime { get; set; }
        public decimal Amount { get; set; }
        public decimal MinimalTimeRate { get; set; }
        public decimal OvertimeRate { get; set; }
        public decimal FineRate { get; set; }
    }
}
