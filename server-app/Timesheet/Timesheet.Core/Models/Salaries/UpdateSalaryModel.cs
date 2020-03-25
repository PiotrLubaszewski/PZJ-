namespace Timesheet.Core.Models.Salaries
{
    public class UpdateSalaryModel
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public decimal MinimumTimeRate { get; set; }
        public decimal OvertimeRate { get; set; }
        public decimal FineRate { get; set; }
    }
}
