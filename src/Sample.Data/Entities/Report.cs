namespace Sample.Data.Entities;

public class Report
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    public DateTimeOffset CreatedAt { get; set; }

    public Guid TenantId { get; set; }
}
