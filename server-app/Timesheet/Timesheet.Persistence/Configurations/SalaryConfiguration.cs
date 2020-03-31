using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Timesheet.Persistence.Entities;

namespace Timesheet.Persistence.Configurations
{
    class SalaryConfiguration : IEntityTypeConfiguration<Salary>
    {
        public void Configure(EntityTypeBuilder<Salary> builder)
        {
            builder.Property(x => x.EndDateTime).IsRequired(false);
            builder.Property(x => x.Amount).IsRequired().HasColumnType("decimal(9, 2)");
            builder.Property(x => x.MinimalTimeRate).IsRequired().HasColumnType("decimal(5, 2)");
            builder.Property(x => x.OvertimeRate).IsRequired().HasColumnType("decimal(5, 2)");
            builder.Property(x => x.FineRate).IsRequired().HasColumnType("decimal(5, 2)");
            builder.HasOne(x => x.User).WithMany(x => x.Salaries).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
