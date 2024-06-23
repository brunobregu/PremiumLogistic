namespace PremiumLogistic_BAL.Services;

public class AuditLogsService : IAuditLogsService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public AuditLogsService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;

    }
    public async Task AddLogs(AddAuditLogsDto auditLogsDto)
    {
        var auditLogs = _mapper.Map<AuditLogs>(auditLogsDto);
        _unitOfWork.AuditLogsRepository.Insert(auditLogs);
        await _unitOfWork.CommitAsync();
    }
}
