using Sample.App.Notifications;

namespace Sample.App.Repositories;

public interface IReportAuditRepository
{
    Task RecordAsync(ReportGeneratedNotification notification, CancellationToken cancellationToken);
}
