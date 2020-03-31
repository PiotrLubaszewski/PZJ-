using Microsoft.Extensions.DependencyInjection;
using Timesheet.Core.Interfaces.Services;

namespace Timesheet.Api.Infrastructure.Extensions.Startup
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.Scan(scan => scan
                .FromAssemblyOf<IAccountsService>()
                    .AddClasses(classes => classes.Where(x => x.Name.EndsWith("Service")))
                        .AsImplementedInterfaces()
                        .WithScopedLifetime());

            return services;
        }
    }
}
