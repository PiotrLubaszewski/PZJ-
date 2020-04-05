using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using Timesheet.Persistence.Entities;

namespace Timesheet.Persistence.Configurations
{
    class UserProjectConfiguration : IEntityTypeConfiguration<UserProject>
    {
        public void Configure(EntityTypeBuilder<UserProject> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CreatedDateTime).IsRequired();

            builder.HasOne(x => x.User).WithMany(x => x.UserProjects).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.Project).WithMany(x => x.UserProjects).HasForeignKey(x => x.ProjectId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
