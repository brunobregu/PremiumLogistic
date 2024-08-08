namespace PremiumLogistic_BAL;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        #region Auction
        CreateMap<Auction, AuctionDto>();

        #endregion

        #region Authentication
        CreateMap<RegisterDto, ApplicationUser>()
            .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => DateTime.Now));

        CreateMap<ApplicationUser, UsersOfRoleDto>();

        CreateMap<IdentityRole, RolesDto>();

        CreateMap<AddRoleDto, ApplicationRole>()
            .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => DateTime.Now));

        CreateMap<CreateUserDto, ApplicationUser>()
            .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => DateTime.Now));
        #endregion

        #region Contact
        CreateMap<AddContactDto, Contact>()
            .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => DateTime.Now));
        #endregion

        #region OrderDetails
        CreateMap<AddOrderDetailsDto, OrderDetails>()
            .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => DateTime.Now));

        CreateMap<OrderDetails, OrderDetailsDto>();

        CreateMap<OrderDetails, AllOrderDetailsDto>()
            .ForMember(dest => dest.Fullname, opt => opt.MapFrom(src => src.User.FirstName + " " + src.User.LastName))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User.Id));

        CreateMap<OrderDetails, OrderDetailsByIdDto>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.User.FirstName + " " + src.User.LastName))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User.Id));

        CreateMap<OrderDetails, MyOrderDetailsByIdDto>();
        #endregion

        #region Port
        CreateMap<Port, PortDto>();
        #endregion

        #region Provider
        CreateMap<Provider, ProviderDto>();
        #endregion

        #region Transportation
        CreateMap<Transportation, LocalTransportationDto>();
        #endregion

        CreateMap(typeof(PagedResponseOffset<>), typeof(PagedResponseOffsetDto<>));

        CreateMap<AddAuditLogsDto, AuditLogs>();
    }
}
