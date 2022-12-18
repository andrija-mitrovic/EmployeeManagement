using EmployeeManagement.Application.Common.DTOs;
using EmployeeManagement.Application.Common.Helpers;
using MediatR;

namespace EmployeeManagement.Application.Employees.Queries.GetEmployeesWithPagination
{
    public sealed class GetEmployeesWithPaginationQuery : IRequest<PaginatedList<EmployeeDto>>
    {
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    }
}
