using MediatR;
using Microsoft.Extensions.Logging;
using Sample.App.Repositories;

namespace Sample.App.Notifications;

public class ReportGeneratedHandler : INotificationHandler<ReportGeneratedNotification>
{
    private readonly IReportAuditRepository _auditRepository;
    private readonly ILogger<ReportGeneratedHandler> _logger;

    public ReportGeneratedHandler(IReportAuditRepository auditRepository, ILogger<ReportGeneratedHandler> logger)
    {
        _auditRepository = auditRepository;
        _logger = logger;
    }

    public async Task Handle(ReportGeneratedNotification notification, CancellationToken cancellationToken)
    {
        await _auditRepository.RecordAsync(notification, cancellationToken);
        _logger.LogInformation("Recorded audit event for report {ReportId}", notification.ReportId);
    }
}
