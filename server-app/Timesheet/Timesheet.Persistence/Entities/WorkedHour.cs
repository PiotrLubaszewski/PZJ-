using System;
using Timesheet.Persistence.Entities.Identities;

namespace Timesheet.Persistence.Entities
{
    public class WorkedHour : IEntity<int>
    {
        public int Id { get; set; }
        public DateTime CreatedDateTime { get; set; }

        public DateTime Date { get; set; }
        public decimal HoursQuantity { get; set; }

        public Guid UserId { get; set; }
        public int ProjectId { get; set; }
        public int ProjectTaskId { get; set; }

        public ApplicationUser User { get; set; }
        public Project Project { get; set; }
        public ProjectTask ProjectTask { get; set; }
    }
}
