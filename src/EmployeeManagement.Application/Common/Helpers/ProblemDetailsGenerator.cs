using EmployeeManagement.Application.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Application.Common.Helpers
{
    public static class ProblemDetailsGenerator
    {

        public static ProblemDetails Generate(Exception exception)
        {
            return exception switch
            {
                ValidationException => GenerateProblemDetailsFor400BadRequest(exception),
                NotFoundException => GenerateProblemDetailsFor404NotFound(exception),
                UnauthorizedAccessException => GenerateProblemDetailsFor401Unauthorized(exception),
                ForbiddenAccessException => GenerateProblemDetailsFor403Forbidden(exception),
                _ => GenerateProblemDetailsFor500InternalServerError(exception)
            };
        }

        private static ProblemDetails GenerateProblemDetailsFor400BadRequest(Exception exception) =>
            new()
            {
                Status = StatusCodes.Status400BadRequest,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                Title = "Bad request",
                Detail = exception.Message
            };

        private static ProblemDetails GenerateProblemDetailsFor500InternalServerError(Exception exception) =>
            new()
            {
                Status = StatusCodes.Status500InternalServerError,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
                Title = "Internal Server Error",
                Detail = exception.Message
            };

        private static ProblemDetails GenerateProblemDetailsFor404NotFound(Exception exception) =>
            new()
            {
                Status = StatusCodes.Status404NotFound,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                Title = "The specified resource was not found.",
                Detail = exception.Message
            };

        private static ProblemDetails GenerateProblemDetailsFor401Unauthorized(Exception exception) =>
            new()
            {
                Status = StatusCodes.Status401Unauthorized,
                Title = "Unauthorized",
                Type = "https://tools.ietf.org/html/rfc7235#section-3.1"
            };

        private static ProblemDetails GenerateProblemDetailsFor403Forbidden(Exception ex) =>
            new()
            {
                Status = StatusCodes.Status403Forbidden,
                Title = "Forbidden",
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.3"
            };
    }
}
