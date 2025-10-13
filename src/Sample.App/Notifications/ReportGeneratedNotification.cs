using MediatR;

namespace Sample.App.Notifications;

public sealed record ReportGeneratedNotification(Guid ReportId, string TenantId, string Title, DateTimeOffset CreatedAt) : INotification;
