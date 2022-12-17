using MediatR;

namespace EmployeeManagement.Application.Employees.Commands.CreateEmployee
{
    public sealed class CreateEmployeeCommand : IRequest<int>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Title { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
    }
}
