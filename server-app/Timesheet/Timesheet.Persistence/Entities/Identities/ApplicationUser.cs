namespace Timesheet.Persistence.Entities.Identities
{
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;

    public class ApplicationUser : IdentityUser<Guid>, IEntity<Guid>
    {
        public DateTime CreatedDateTime { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<Salary> Salaries { get; set; }
        public ICollection<UserProject> UserProjects { get; set; }
        public ICollection<ProjectTask> ProjectTasks { get; set; }
    }
}
