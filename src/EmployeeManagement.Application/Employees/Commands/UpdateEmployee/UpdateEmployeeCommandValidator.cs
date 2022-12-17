using EmployeeManagement.Application.Employees.Commands.CreateEmployee;
using FluentValidation;

namespace EmployeeManagement.Application.Employees.Commands.UpdateEmployee
{
    internal class UpdateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
    {
        public UpdateEmployeeCommandValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.Address).NotEmpty();
        }
    }
}
