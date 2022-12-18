using EmployeeManagement.Application.Common.DTOs;
using MediatR;

namespace EmployeeManagement.Application.Employees.Queries.GetEmployeeById
{
    public sealed class GetEmployeeByIdQuery : IRequest<EmployeeDto>
    {
        public int Id { get; set; }
    }
}
