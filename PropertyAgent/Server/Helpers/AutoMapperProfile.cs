namespace PropertyAgent.Server.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<EmployeeDto, AppUser>();
            CreateMap<AppUser, EmployeeDto>()
                .ForMember(dest => dest.PropertyDtos, opt => opt.MapFrom(src => src.Properties));
            CreateMap<EmployeeUpdateDto,AppUser>();
            CreateMap<PropertyDto, Property>();
            CreateMap<Property, PropertyDto>()
                .ForMember(dest => dest.Employee, opt => opt.MapFrom(src => src.AppUser));
            CreateMap<PhotoDto, Photo>();
            CreateMap<Photo, PhotoDto>();
           
        }
    }
}
