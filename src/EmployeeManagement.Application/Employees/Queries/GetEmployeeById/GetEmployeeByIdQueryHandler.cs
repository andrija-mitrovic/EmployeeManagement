using AutoMapper;
using EmployeeManagement.Application.Common.DTOs;
using EmployeeManagement.Application.Common.Exceptions;
using EmployeeManagement.Application.Common.Interfaces;
using EmployeeManagement.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement.Application.Employees.Queries.GetEmployeeById
{
    internal sealed class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, EmployeeDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<GetEmployeeByIdQueryHandler> _logger;

        public GetEmployeeByIdQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            ILogger<GetEmployeeByIdQueryHandler> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<EmployeeDto> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            var employee = await _context.Employees.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (employee == null)
            {
                _logger.LogError(nameof(Employee) + " with Id: {EmployeeId} was not found.", request.Id);
                throw new EmployeeNotFoundException(request.Id);
            }

            return _mapper.Map<EmployeeDto>(employee);
        }
    }
}
