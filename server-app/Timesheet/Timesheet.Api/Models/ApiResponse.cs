namespace Timesheet.Api.Models
{
    using System.Collections.Generic;
    using System.Net;

    public class ApiResponse<T>
    {
        public T Result { get; set; }
        public int StatusCode { get; set; }
        public string Error { get; set; }
        public IDictionary<string, IEnumerable<string>> ValidationErrors { get; set; }
    }

    public class ApiResponse : ApiResponse<string>
    {
        public static ApiResponse<string> CreateValidationErrorsResponse(IDictionary<string, IEnumerable<string>> errors)
        {
            return new ApiResponse<string>
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                ValidationErrors = errors
            };
        }

        public static ApiResponse<string> CreateErrorResponse(string error)
        {
            return new ApiResponse<string>
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Error = error
            };
        }

        public static ApiResponse<string> CreateErrorResponse(string error, HttpStatusCode statusCode)
        {
            return new ApiResponse<string>
            {
                StatusCode = (int)statusCode,
                Error = error
            };
        }

        public static ApiResponse<T> CreateResultResponse<T>(T result)
        {
            return new ApiResponse<T>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Result = result
            };
        }

        public static ApiResponse CreateResultResponse()
        {
            return new ApiResponse
            {
                StatusCode = (int)HttpStatusCode.OK
            };
        }
    }
}
