using AutoMapper;
using EmployeeManagement.Application.Common.Exceptions;
using EmployeeManagement.Application.Common.Interfaces;
using EmployeeManagement.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement.Application.Employees.Commands.UpdateEmployee
{
    internal sealed class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateEmployeeCommandHandler> _logger;

        public UpdateEmployeeCommandHandler(
            IEmployeeRepository employeeRepository, 
            IUnitOfWork unitOfWork, 
            IMapper mapper, 
            ILogger<UpdateEmployeeCommandHandler> logger)
        {
            _employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetByIdAsync(request.Id, cancellationToken);

            if (employee == null)
            {
                _logger.LogError(nameof(Employee) + " with Id: {ProductId} was not found.", request.Id);
                throw new NotFoundException(nameof(Employee), request.Id);
            }

            _mapper.Map(request, employee);

            _employeeRepository.Update(employee);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
