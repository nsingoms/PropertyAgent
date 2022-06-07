namespace PropertyAgent.Server.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<EmployeeDto, AppUser>();
            CreateMap<AppUser, EmployeeDto>();
            CreateMap<EmployeeUpdateDto,AppUser>();
            CreateMap<PropertyDto, Property>();
            CreateMap<Property, PropertyDto>();
            CreateMap<PhotoDto, Photo>();
            CreateMap<Photo, PhotoDto>();
           
        }
    }
}
