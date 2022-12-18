using AutoMapper;
using AutoMapper.QueryableExtensions;
using EmployeeManagement.Application.Common.DTOs;
using EmployeeManagement.Application.Common.Helpers;
using EmployeeManagement.Application.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EmployeeManagement.Application.Employees.Queries.GetEmployeesWithPagination
{
    internal sealed class GetEmployeesWithPaginationQueryHandler : IRequestHandler<GetEmployeesWithPaginationQuery, PaginatedList<EmployeeDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetEmployeesWithPaginationQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<PaginatedList<EmployeeDto>> Handle(GetEmployeesWithPaginationQuery request, CancellationToken cancellationToken)
        {
            var products = await _context.Employees.ProjectTo<EmployeeDto>(_mapper.ConfigurationProvider)
                                         .PaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);

            _httpContextAccessor.HttpContext.Response.AddPaginationHeader(request.PageNumber, request.PageSize, products.TotalCount, products.TotalPages);

            return products;
        }
    }
}
