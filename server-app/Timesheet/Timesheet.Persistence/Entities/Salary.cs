using System;
using Timesheet.Persistence.Entities.Identities;

namespace Timesheet.Persistence.Entities
{
    public class Salary
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public decimal Amount { get; set; }
        public decimal MinimumTimeRate { get; set; }
        public decimal OvertimeRate { get; set; }
        public decimal FineRate { get; set; }

        public ApplicationUser User { get; set; }
    }
}
