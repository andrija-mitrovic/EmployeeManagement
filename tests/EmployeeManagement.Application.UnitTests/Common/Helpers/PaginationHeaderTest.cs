using EmployeeManagement.Application.Common.Helpers;
using FluentAssertions;

namespace EmployeeManagement.Application.UnitTests.Common.Helpers
{
    public sealed class PaginationHeaderTest
    {
        [Fact]
        public void DefaultConstructorCreatesPaginationHeaderInstance()
        {
            var result = new PaginationHeader(1, 5, 3, 15);

            result.CurrentPage.Should().Be(1);
            result.PageSize.Should().Be(5);
            result.TotalPages.Should().Be(3);
            result.TotalCount.Should().Be(15);
        }
    }
}
