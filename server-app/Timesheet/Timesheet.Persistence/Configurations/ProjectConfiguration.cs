using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using Timesheet.Persistence.Entities;

namespace Timesheet.Persistence.Configurations
{
    class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CreatedDateTime).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(255);
        }
    }
}
