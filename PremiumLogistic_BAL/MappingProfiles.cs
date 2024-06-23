namespace PremiumLogistic_BAL;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Port, PortDto>();
        CreateMap<LocalTransportation, LocalTransportationDto>();
        CreateMap(typeof(PagedResponseOffset<>), typeof(PagedResponseOffsetDto<>));
        CreateMap<AddAuditLogsDto, AuditLogs>();
        CreateMap<AddContactDto, Contact>();
        CreateMap<AddOrderDetailsDto, OrderDetails>();
        CreateMap<ApplicationUser, UsersOfRoleDto>();
        CreateMap<OrderDetails, OrderDetailsDto>();
    }
}
