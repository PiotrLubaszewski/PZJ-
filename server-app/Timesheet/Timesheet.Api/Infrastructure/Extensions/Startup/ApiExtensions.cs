namespace Timesheet.Api.Infrastructure.Extensions.Startup
{
    using FluentValidation.AspNetCore;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using System.Collections.Generic;
    using System.Linq;
    using Timesheet.Api.Models;
    using Timesheet.Api.Validators.Accounts;

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
                            var keyCamelCase = key[0].ToString().ToLower() + key.Substring(1, key.Length - 1);
                            if (value.Errors.Any()) errors.Add(keyCamelCase, value.Errors.Select(x => x.ErrorMessage));
                        }

                        return new BadRequestObjectResult(ApiResponse.CreateValidationErrorsResponse(errors));
                    };
                });

            return services;
        }
    }
}
