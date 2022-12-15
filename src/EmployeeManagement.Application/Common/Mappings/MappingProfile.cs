using AutoMapper;
using EmployeeManagement.Application.Common.DTOs;
using EmployeeManagement.Domain.Entities;

namespace EmployeeManagement.Application.Common.Mappings
{
    public sealed class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeDto>().ReverseMap();
        }
    }
}
