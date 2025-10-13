using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Sample.App.Notifications;
using Sample.Data.Entities;
using Sample.Data.Infrastructure;

namespace Sample.App.Repositories;

public class ReportAuditRepository : IReportAuditRepository
{
    private readonly ReportsDbContext _dbContext;
    private readonly IMapper _mapper;

    public ReportAuditRepository(ReportsDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task RecordAsync(ReportGeneratedNotification notification, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<ReportAudit>(notification);
        entity.Id = Guid.NewGuid();
        entity.RecordedAt = DateTimeOffset.UtcNow;
        _dbContext.ReportAudits.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
