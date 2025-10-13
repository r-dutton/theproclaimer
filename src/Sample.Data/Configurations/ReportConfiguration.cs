using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.Data.Entities;

namespace Sample.Data.Configurations;

public class ReportConfiguration : IEntityTypeConfiguration<Report>
{
    public void Configure(EntityTypeBuilder<Report> builder)
    {
        builder.ToTable("Reports");
        builder.HasKey(r => r.Id);
        builder.HasIndex(r => new { r.TenantId, r.CreatedAt });
        builder.Property(r => r.CreatedAt)
            .HasConversion(v => v.UtcDateTime, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        builder.Property(r => r.Summary)
            .HasMaxLength(512);
        builder.Property(r => r.Metadata.Classification)
            .HasColumnName("Classification")
            .HasMaxLength(64);
        builder.Property(r => r.Metadata.SourceSystem)
            .HasColumnName("SourceSystem")
            .HasMaxLength(128);
        builder.HasQueryFilter(r => r.TenantId == TenantProvider.CurrentTenant);
    }
}

public static class TenantProvider
{
    public const string CurrentTenant = "tenant-001";
}
