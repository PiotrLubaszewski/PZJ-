using System;

namespace Timesheet.Core.Models.Salaries
{
    public class SalaryModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public decimal Amount { get; set; }
        public decimal MinimumTimeRate { get; set; }
        public decimal OvertimeRate { get; set; }
        public decimal FineRate { get; set; }
    }
}
