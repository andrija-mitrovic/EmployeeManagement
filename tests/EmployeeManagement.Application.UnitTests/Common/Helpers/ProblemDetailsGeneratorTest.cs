using EmployeeManagement.Application.Common.Exceptions;
using EmployeeManagement.Application.Common.Helpers;
using FluentAssertions;
using Microsoft.AspNetCore.Http;

namespace EmployeeManagement.Application.UnitTests.Common.Helpers
{
    public sealed class ProblemDetailsGeneratorTest
    {
        [Fact]
        public void Generate_ShouldReturnProblemDetailsWithStatus400BadRequest_WhenRequestIsValidationException()
        {
            var exception = new ValidationException();

            var result = ProblemDetailsGenerator.Generate(exception);

            result.Status.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public void Generate_ShouldReturnProblemDetailsWithStatus404NotFound_WhenRequestIsNotFoundException()
        {
            var exception = new NotFoundException();

            var result = ProblemDetailsGenerator.Generate(exception);

            result.Status.Should().Be(StatusCodes.Status404NotFound);
        }

        [Fact]
        public void Generate_ShouldReturnProblemDetailsWithStatus401Unauthorized_WhenRequestIsUnauthorizedAccessException()
        {
            var exception = new UnauthorizedAccessException();

            var result = ProblemDetailsGenerator.Generate(exception);

            result.Status.Should().Be(StatusCodes.Status401Unauthorized);
        }

        [Fact]
        public void Generate_ShouldReturnProblemDetailsWithStatus403Forbidden_WhenRequestIsForbiddenAccessException()
        {
            var exception = new ForbiddenAccessException();

            var result = ProblemDetailsGenerator.Generate(exception);

            result.Status.Should().Be(StatusCodes.Status403Forbidden);
        }

        [Fact]
        public void Generate_ShouldReturnProblemDetailsStatus500InternalServerError_WhenRequestIsInternalException()
        {
            var exception = new Exception();

            var result = ProblemDetailsGenerator.Generate(exception);

            result.Status.Should().Be(StatusCodes.Status500InternalServerError);
        }
    }
}
