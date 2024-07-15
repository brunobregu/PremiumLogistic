namespace PremiumLogistic_BAL;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<RegisterDto, ApplicationUser>()
            .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => DateTime.Now));
        CreateMap<Transportation, LocalTransportationDto>();
        CreateMap(typeof(PagedResponseOffset<>), typeof(PagedResponseOffsetDto<>));
        CreateMap<AddAuditLogsDto, AuditLogs>();
        CreateMap<AddContactDto, Contact>()
            .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => DateTime.Now));
        CreateMap<AddOrderDetailsDto, OrderDetails>()
            .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => DateTime.Now));
        CreateMap<ApplicationUser, UsersOfRoleDto>();
        CreateMap<OrderDetails, OrderDetailsDto>();
        CreateMap<IdentityRole, RolesDto>();
        CreateMap<AddRoleDto, ApplicationRole>()
            .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => DateTime.Now));
        CreateMap<CreateUserDto, ApplicationUser>()
            .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => DateTime.Now));
        CreateMap<OrderDetails, AllOrderDetailsDto>()
            .ForMember(dest => dest.Fullname, opt => opt.MapFrom(src => src.User.FirstName + " " + src.User.LastName));
        CreateMap<OrderDetails, OrderDetailsByIdDto>();
    }
}
