using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Timesheet.Persistence.Entities;

namespace Timesheet.Persistence.Configurations
{
    class WorkedHourConfiguration : IEntityTypeConfiguration<WorkedHour>
    {
        public void Configure(EntityTypeBuilder<WorkedHour> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CreatedDateTime).IsRequired();

            builder.HasOne(x => x.User).WithMany(x => x.WorkedHours).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.Project).WithMany(x => x.WorkedHours).HasForeignKey(x => x.ProjectId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.ProjectTask).WithMany(x => x.WorkedHours).HasForeignKey(x => x.ProjectTaskId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
