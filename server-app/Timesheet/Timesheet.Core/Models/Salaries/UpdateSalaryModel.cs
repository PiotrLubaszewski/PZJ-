namespace Timesheet.Core.Models.Salaries
{
    public class UpdateSalaryModel
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public decimal MinimalTimeRate { get; set; }
        public decimal OvertimeRate { get; set; }
        public decimal FineRate { get; set; }
    }
}
