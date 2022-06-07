using AutoMapper;

namespace PropertyAgent.Client.Helpers
{
    public class ProfileMapper : Profile
    {
        public ProfileMapper()
        {
            CreateMap<EmployeeDto, EmployeeUpdateDto>();
        }
    }
}
