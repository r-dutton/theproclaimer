using Microsoft.EntityFrameworkCore;
using Sample.Data.Entities;
using Sample.Data.Configurations;

namespace Sample.Data.Infrastructure;

public static class SeedData
{
    public static async Task EnsureSeededAsync(ReportsDbContext context)
    {
        await context.Database.EnsureCreatedAsync();
        if (!await context.Reports.AnyAsync())
        {
            context.Reports.Add(new Report
            {
                Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                Title = "Quarterly Report",
                Body = "Revenue increased by 12%.",
                Summary = "Quarterly performance summary",
                CreatedAt = DateTimeOffset.UtcNow.AddDays(-1),
                TenantId = TenantProvider.CurrentTenant,
                Metadata = new ReportMetadata
                {
                    Classification = "confidential",
                    SourceSystem = "erp"
                }
            });

            await context.SaveChangesAsync();
        }
    }
}
