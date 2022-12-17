using AutoMapper;
using EmployeeManagement.Application.Common.Interfaces;
using EmployeeManagement.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement.Application.Employees.Commands.CreateEmployee
{
    internal sealed class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, int>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateEmployeeCommandHandler> _logger;

        public CreateEmployeeCommandHandler(
            IEmployeeRepository employeeRepository, 
            IUnitOfWork unitOfWork, 
            IMapper mapper, 
            ILogger<CreateEmployeeCommandHandler> logger)
        {
            _employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<int> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = _mapper.Map<Employee>(request);

            await _employeeRepository.AddAsync(employee, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation(nameof(Employee) + " with Id: {EmployeeId} is successfully created.", employee.Id);

            return employee.Id;
        }
    }
}
