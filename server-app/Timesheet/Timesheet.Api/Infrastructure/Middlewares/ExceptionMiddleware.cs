namespace Timesheet.Api.Infrastructure.Middlewares
{
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using Timesheet.Api.Infrastructure.Utils;
    using Timesheet.Api.Models;

    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var response = JsonUtils.SerializeObjectWithCamelCasePropertyNames(ApiResponse.CreateErrorResponse(ex.Message));

                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(response);
            }
        }
    }
}
