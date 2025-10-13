using Microsoft.EntityFrameworkCore;
using Sample.Data.Configurations;
using Sample.Data.Entities;

namespace Sample.Data.Infrastructure;

public class ReportsDbContext : DbContext
{
    public ReportsDbContext(DbContextOptions<ReportsDbContext> options)
        : base(options)
    {
    }

    public DbSet<Report> Reports => Set<Report>();
    public DbSet<ReportAudit> ReportAudits => Set<ReportAudit>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ReportConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}
