using Microsoft.EntityFrameworkCore;
using Sample.Data.Entities;

namespace Sample.Data.Db;

public class ReportsDbContext : DbContext
{
    public ReportsDbContext(DbContextOptions<ReportsDbContext> options)
        : base(options)
    {
    }

    public DbSet<Report> Reports => Set<Report>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Report>(builder =>
        {
            builder.ToTable("Reports");
            builder.HasKey(r => r.Id);
            builder.HasQueryFilter(r => r.TenantId != Guid.Empty);
        });
    }
}
