namespace Sample.Msg.Contracts;

public record ReportPublished(Guid ReportId, string TenantId, string Title, DateTimeOffset PublishedAt);
