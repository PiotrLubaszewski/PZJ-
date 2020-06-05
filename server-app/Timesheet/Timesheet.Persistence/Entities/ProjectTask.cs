using System;
using System.Collections.Generic;
using Timesheet.Persistence.Entities.Identities;

namespace Timesheet.Persistence.Entities
{
    public class ProjectTask : IEntity<int>
    {
        public int Id { get; set; }
        public DateTime CreatedDateTime { get; set; }

        public int ProjectId { get; set; }
        public Guid UserId { get; set; }

        public string Name { get; set; }

        public Project Project { get; set; }
        public ApplicationUser User { get; set; }

        public ICollection<WorkedHour> WorkedHours { get; set; }
    }
}
