﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using Timesheet.Persistence.Entities.Identities;

namespace Timesheet.Persistence.Configurations
{
    class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CreatedDateTime).IsRequired();
            builder.Property(x => x.FirstName).HasMaxLength(200);
            builder.Property(x => x.LastName).HasMaxLength(200);
        }
    }
}
