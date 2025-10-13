using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Sample.Data.Entities;

[Table("Reports")]
public class Report
{
    [Key]
    public Guid Id { get; set; }

    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(4000)]
    public string Body { get; set; } = string.Empty;

    [MaxLength(512)]
    public string Summary { get; set; } = string.Empty;

    public DateTimeOffset CreatedAt { get; set; }

    public string TenantId { get; set; } = string.Empty;

    public ReportMetadata Metadata { get; set; } = new();
}

[Owned]
public class ReportMetadata
{
    public string Classification { get; set; } = "public";

    public string SourceSystem { get; set; } = "unknown";
}
