namespace Timesheet.Api
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Timesheet.Api.Infrastructure.Extensions.Startup;
    using Timesheet.Api.Infrastructure.Middlewares;
    using Timesheet.Api.Models.Settings;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Configure ASP.NET WebApi with Fluent Validation
            services.AddAndConfigureApi();

            // Configure DbContext and Identity
            services.AddContextAndIdentity(Configuration);

            // Add Swagger
            services.AddSwagger();

            // Get JWT Settings and configure Authentication
            var jwtSettings = Configuration.GetSection("JwtSettings").Get<JwtSettings>();
            services.AddSingleton(jwtSettings);
            services.AddJwtBearer(jwtSettings);

            // Register all services (classes thats name ends with 'Service')
            services.AddApplicationServices();

            // Add Cors policy
            services.AddCorsPolicy();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwaggerAndSwaggerUI();
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
