using EmployeeManagement.Application.Common.Helpers;
using EmployeeManagement.Domain.Entities;
using FluentAssertions;

namespace EmployeeManagement.Application.UnitTests.Common.Helpers
{
    public sealed class PaginatedListTest
    {
        [Fact]
        public void DefaultConstructorCreatesPaginatedListInstance()
        {
            List<Employee> employees = ReturnEmployees();

            var result = new PaginatedList<Employee>(employees, 4, 1, 2);

            result.PageNumber.Should().Be(1);
            result.TotalPages.Should().Be(2);
            result.TotalCount.Should().Be(4);
        }

        private static List<Employee> ReturnEmployees()
        {
            return new List<Employee>()
            {
                new Employee()
                {
                    FirstName = "Andrija",
                    LastName = "Mitrovic",
                    Title = "Software engineer",
                    Address = "Herceg Novi",
                    Email = "andrija@gmail.com"
                },
                new Employee()
                {
                    FirstName = "Marko",
                    LastName = "Markovic",
                    Title = "Project manager",
                    Address = "Podgorica",
                    Email = "marko@gmail.com"
                },
                new Employee()
                {
                    FirstName = "Ivan",
                    LastName = "Ivanovic",
                    Title = "Software engineer",
                    Address = "Kotor",
                    Email = "ivan@gmail.com"
                },
                new Employee()
                {
                    FirstName = "Petar",
                    LastName = "Petrovic",
                    Title = "Project manager",
                    Address = "Niksic",
                    Email = "petar@gmail.com"
                }
            };
        }
    }
}
