namespace Timesheet.Api.Infrastructure.Extensions.Startup
{
    using FluentValidation.AspNetCore;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using System.Collections.Generic;
    using System.Linq;
    using Timesheet.Api.Models;
    using Timesheet.Api.Validators.Accounts;
    using Timesheet.Core.Extensions;

    public static class ApiExtensions
    {
        public static IServiceCollection AddAndConfigureApi(this IServiceCollection services)
        {
            services
                .AddControllers()
                .AddFluentValidation(options =>
                {
                    options.RegisterValidatorsFromAssemblyContaining<AuthorizeQueryValidator>();
                    options.LocalizationEnabled = false;
                })
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.InvalidModelStateResponseFactory = context =>
                    {
                        var errors = new Dictionary<string, IEnumerable<string>>();

                        foreach (var key in context.ModelState.Keys)
                        {
                            var value = context.ModelState[key];
                            var keyCamelCase = key?.ToCamelCase();
                            if (value.Errors.Any()) errors.Add(keyCamelCase, value.Errors.Select(x => x.ErrorMessage));
                        }

                        return new BadRequestObjectResult(ApiResponse.CreateValidationErrorsResponse(errors));
                    };
                });

            services.AddRazorPages();

            return services;
        }
    }
}
