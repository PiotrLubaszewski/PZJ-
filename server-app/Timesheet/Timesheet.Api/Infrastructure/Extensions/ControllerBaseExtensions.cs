using Microsoft.AspNetCore.Mvc;
using Timesheet.Api.Models;

namespace Timesheet.Api.Infrastructure.Extensions
{
    public static class ControllerBaseExtensions
    {
        public static ApiResponse Result(this ControllerBase controller)
        {
            return ApiResponse.CreateResultResponse();
        }

        public static ApiResponse<T> Result<T>(this ControllerBase controller, T result)
        {
            return ApiResponse.CreateResultResponse(result);
        }
    }
}
