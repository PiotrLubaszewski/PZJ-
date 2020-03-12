namespace Timesheet.Persistence
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using System;
    using Timesheet.Persistence.Configurations;
    using Timesheet.Persistence.Entities.Identities;

    /// <summary>
    /// Partial class that contains EF DbContext configuration
    /// </summary>
    public partial class TimesheetContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public TimesheetContext(DbContextOptions<TimesheetContext> options) : base(options) { }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationUserConfiguration).Assembly);
        }
    }
}
