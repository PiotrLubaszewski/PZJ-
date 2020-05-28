using System;
using System.Collections.Generic;

namespace Timesheet.Persistence.Entities
{
    public class Project : IEntity<int>
    {
        public int Id { get; set; }
        public DateTime CreatedDateTime { get; set; }

        public string Name { get; set; }

        public ICollection<UserProject> UserProjects { get; set; }
        public ICollection<ProjectTask> ProjectTasks { get; set; }
    }
}
