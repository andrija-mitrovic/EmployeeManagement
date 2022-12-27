using EmployeeManagement.Application.Common.Helpers;
using FluentAssertions;

namespace EmployeeManagement.Application.UnitTests.Common.Helpers
{
    public sealed class HelperFunctionTest
    {
        [Theory]
        [InlineData("CreateEmployee")]
        public void GetMethodName_ShouldGetMethodName_WhenArgumentIsPassed(string input)
        {
            var result = HelperFunction.GetMethodName(input);

            result.Should().BeEquivalentTo(input);
        }
    }
}
