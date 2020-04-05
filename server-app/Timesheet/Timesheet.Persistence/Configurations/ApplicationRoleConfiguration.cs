using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Timesheet.Persistence.Entities.Identities;

namespace Timesheet.Persistence.Configurations
{
    class ApplicationRoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CreatedDateTime).IsRequired();
        }
    }
}
