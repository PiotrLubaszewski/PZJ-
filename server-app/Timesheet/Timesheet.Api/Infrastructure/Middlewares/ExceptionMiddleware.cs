namespace Timesheet.Api.Infrastructure.Middlewares
{
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;
    using Timesheet.Api.Infrastructure.Utils;
    using Timesheet.Api.Models;
    using Timesheet.Core.Exceptions;
    using Timesheet.Core.Extensions;

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
            catch (InvalidValidationException ex)
            {
                var dictionary = new Dictionary<string, IEnumerable<string>> { { ex.PropertyName.ToCamelCase(), new List<string> { ex.Message } } };
                var response = JsonUtils.SerializeObjectWithCamelCasePropertyNames(ApiResponse.CreateValidationErrorsResponse(dictionary));

                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(response);
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
