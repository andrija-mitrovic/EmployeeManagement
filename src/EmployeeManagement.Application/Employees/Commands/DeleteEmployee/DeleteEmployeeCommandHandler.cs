using EmployeeManagement.Application.Common.Exceptions;
using EmployeeManagement.Application.Common.Interfaces;
using EmployeeManagement.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement.Application.Employees.Commands.DeleteEmployee
{
    internal sealed class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteEmployeeCommandHandler> _logger;

        public DeleteEmployeeCommandHandler(
            IEmployeeRepository employeeRepository, 
            IUnitOfWork unitOfWork, 
            ILogger<DeleteEmployeeCommandHandler> logger)
        {
            _employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetByIdAsync(request.Id, cancellationToken);

            if (employee == null)
            {
                _logger.LogError(nameof(Employee) + " with Id: {EmployeeId} was not found.", request.Id);
                throw new NotFoundException(nameof(Employee), request.Id);
            }

            _employeeRepository.Delete(employee);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation(nameof(employee) + " with Id: {EmployeeId} is successfully deleted.", employee.Id);

            return Unit.Value;
        }
    }
}
