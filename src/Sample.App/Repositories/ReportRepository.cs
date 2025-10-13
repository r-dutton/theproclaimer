using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Sample.App.Commands;
using Sample.App.Options;
using Sample.Data.Entities;
using Sample.Data.Infrastructure;

namespace Sample.App.Repositories;

public class ReportRepository : IReportRepository
{
    private readonly ReportsDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IMemoryCache _cache;
    private readonly IOptions<ReportRetentionOptions> _options;

    public ReportRepository(ReportsDbContext dbContext, IMapper mapper, IMemoryCache cache, IOptions<ReportRetentionOptions> options)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _cache = cache;
        _options = options;
    }

    public async Task<Report> CreateDetailedAsync(CreateDetailedReportCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Report>(command);
        entity.Id = Guid.NewGuid();
        entity.CreatedAt = DateTimeOffset.UtcNow;
        entity.Metadata = new ReportMetadata
        {
            Classification = "confidential",
            SourceSystem = "web"
        };

        _dbContext.Reports.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        _cache.Remove(BuildCacheKey(entity.Id));
        return entity;
    }

    public async Task<int> PruneOldAsync(DateTimeOffset cutoff, CancellationToken cancellationToken)
    {
        var expired = await _dbContext.Reports
            .Where(report => report.CreatedAt < cutoff)
            .ToListAsync(cancellationToken);

        if (expired.Count == 0)
        {
            return 0;
        }

        foreach (var report in expired)
        {
            _cache.Remove(BuildCacheKey(report.Id));
        }

        _dbContext.Reports.RemoveRange(expired);
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private string BuildCacheKey(Guid id)
        => $"{_options.Value.CachePrefix}:{id}";
}
