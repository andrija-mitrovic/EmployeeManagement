using MediatR;

namespace EmployeeManagement.Application.Employees.Commands.UpdateEmployee
{
    public sealed class UpdateEmployeeCommand : IRequest
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Title { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
    }
}
