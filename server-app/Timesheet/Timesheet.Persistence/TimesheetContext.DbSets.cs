using Microsoft.EntityFrameworkCore;
using Timesheet.Persistence.Entities;

namespace Timesheet.Persistence
{
    /// <summary>
    /// Partial class that contains only DbSets e.g
    /// public DbSet<Test> Tests { get; set; }
    /// </summary>
    public partial class TimesheetContext
    {
        public DbSet<Salary> Salaries { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<UserProject> UserProjects { get; set; }
    }
}
