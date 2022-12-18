using EmployeeManagement.Application.Common.DTOs;
using MediatR;

namespace EmployeeManagement.Application.Employees.Queries.GetEmployees
{
    public sealed class GetEmployeesQuery : IRequest<List<EmployeeDto>>
    {
    }
}
