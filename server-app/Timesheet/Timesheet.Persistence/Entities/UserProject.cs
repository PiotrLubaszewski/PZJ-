using System;
using Timesheet.Persistence.Entities.Identities;

namespace Timesheet.Persistence.Entities
{
    public class UserProject : IEntity<int>
    {
        public int Id { get; set; }
        public DateTime CreatedDateTime { get; set; }

        public Guid UserId { get; set; }
        public int ProjectId { get; set; }

        public ApplicationUser User { get; set; }
        public Project Project { get; set; }
    }
}
