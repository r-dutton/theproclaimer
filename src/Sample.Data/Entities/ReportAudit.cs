using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sample.Data.Entities;

[Table("ReportAudits")]
public class ReportAudit
{
    [Key]
    public Guid Id { get; set; }

    public Guid ReportId { get; set; }

    [MaxLength(128)]
    public string TenantId { get; set; } = string.Empty;

    [MaxLength(256)]
    public string Title { get; set; } = string.Empty;

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset RecordedAt { get; set; }
}
