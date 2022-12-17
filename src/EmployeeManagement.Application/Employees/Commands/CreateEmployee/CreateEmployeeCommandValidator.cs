using FluentValidation;

namespace EmployeeManagement.Application.Employees.Commands.CreateEmployee
{
    internal sealed class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
    {
        public CreateEmployeeCommandValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.Address).NotEmpty();
        }
    }
}
