using EmployeeManagement.Application.Common.Exceptions;
using FluentAssertions;
using FluentValidation.Results;

namespace EmployeeManagement.Application.UnitTests.Common.Exceptions
{
    public sealed class ValidationExceptionTest
    {
        [Fact]
        public void DefaultConstructorCreatesAnEmptyErrorDictionary()
        {
            var actual = new ValidationException().Errors;

            actual.Keys.Should().BeEquivalentTo(Array.Empty<string>());
        }

        [Fact]
        public void SingleValidationFailureCreatesASingleElementErrorDictionary()
        {
            var failures = new List<ValidationFailure>
            {
                new ValidationFailure("FirstName", "First name cannot be empty")
            };

            var actual = new ValidationException(failures).Errors;

            actual.Keys.Should().BeEquivalentTo(new string[] { "FirstName" });
            actual["FirstName"].Should().BeEquivalentTo(new string[] { "First name cannot be empty" });
        }

        [Fact]
        public void MulitpleValidationFailureForMultiplePropertiesCreatesAMultipleElementErrorDictionaryEachWithMultipleValues()
        {
            var failures = new List<ValidationFailure>
            {
                new ValidationFailure("FirstName", "First name cannot be empty"),
                new ValidationFailure("LastName", "Last name cannot be empty"),
            };

            var actual = new ValidationException(failures).Errors;

            actual.Keys.Should().BeEquivalentTo(new string[] { "FirstName", "LastName" });
            actual["FirstName"].Should().BeEquivalentTo(new string[]
            {
                "First name cannot be empty"
            });
            actual["FirstName"].Should().BeEquivalentTo(new string[] { "First name cannot be empty" });
            actual["LastName"].Should().BeEquivalentTo(new string[] { "Last name cannot be empty" });
        }
    }
}