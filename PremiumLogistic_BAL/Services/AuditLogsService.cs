namespace PremiumLogistic_BAL.Services;

public class AuditLogsService(IUnitOfWork unitOfWork, IMapper mapper) : IAuditLogsService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task AddLogs(AddAuditLogsDto auditLogsDto)
    {
        var auditLogs = _mapper.Map<AuditLogs>(auditLogsDto);
        _unitOfWork.AuditLogsRepository.Insert(auditLogs);
        await _unitOfWork.CommitAsync();
    }
}
