namespace Timesheet.Api.Infrastructure.Extensions.Startup
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Timesheet.Persistence;
    using Timesheet.Persistence.Entities.Identities;

    public static class ContextExtensions
    {
        public static IServiceCollection AddContextAndIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddDbContext<TimesheetContext>(options => options.UseSqlServer(configuration.GetConnectionString("TimesheetDatabase")));
            services
                .AddIdentity<ApplicationUser, ApplicationRole>(options =>
                {
                    options.Password.RequiredLength = 6;
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                })
                .AddEntityFrameworkStores<TimesheetContext>()
                .AddDefaultTokenProviders();

            return services;
        }
    }
}
