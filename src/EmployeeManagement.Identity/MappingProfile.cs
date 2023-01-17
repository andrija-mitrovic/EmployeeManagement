using AutoMapper;
using EmployeeManagement.Identity.Entities.ViewModels;
using EmployeeManagement.Identity.Entities;

namespace EmployeeManagement.Identity
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserRegistrationModel, User>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));
        }
    }
}
