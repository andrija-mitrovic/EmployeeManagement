using MediatR;

namespace EmployeeManagement.Application.Employees.Commands.DeleteEmployee
{
    public sealed class DeleteEmployeeCommand : IRequest
    {
        public int Id { get; set; }
    }
}
