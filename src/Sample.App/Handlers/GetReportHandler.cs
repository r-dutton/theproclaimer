using System;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sample.App.Dtos;
using Sample.App.Queries;
using Sample.App.Options;
using Sample.Data.Entities;
using Sample.Data.Infrastructure;
using Sample.Msg.Contracts;
using Sample.Msg.Publishing;

namespace Sample.App.Handlers;

public class GetReportHandler : IRequestHandler<GetReportQuery, ReportDto>
{
    private readonly ReportsDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IReportPublisher _publisher;
    private readonly ILogger<GetReportHandler> _logger;
    private readonly IMemoryCache _cache;
    private readonly IOptions<ReportRetentionOptions> _options;

    public GetReportHandler(
        ReportsDbContext dbContext,
        IMapper mapper,
        IReportPublisher publisher,
        ILogger<GetReportHandler> logger,
        IMemoryCache cache,
        IOptions<ReportRetentionOptions> options)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _publisher = publisher;
        _logger = logger;
        _cache = cache;
        _options = options;
    }

    public async Task<ReportDto> Handle(GetReportQuery request, CancellationToken cancellationToken)
    {
        var cachePrefix = _options.Value.CachePrefix;
        var cacheKey = $"{cachePrefix}:{request.Id}";
        var expiration = TimeSpan.FromMinutes(Math.Max(1, _options.Value.CacheExpirationMinutes));
        var cacheMiss = false;

        var cached = await _cache.GetOrCreateAsync(cacheKey, async entry =>
        {
            cacheMiss = true;
            entry.AbsoluteExpirationRelativeToNow = expiration;

            var entity = await _dbContext.Reports
                .AsNoTracking()
                .FirstAsync(r => r.Id == request.Id, cancellationToken);

            var dtoResult = _mapper.Map<ReportDto>(entity);
            return new CachedReport(entity, dtoResult);
        }) ?? throw new InvalidOperationException("Failed to resolve report from cache or database.");

        if (cacheMiss)
        {
            await _publisher.PublishAsync(new ReportPublished(
                cached.Entity.Id,
                cached.Entity.TenantId,
                cached.Entity.Title,
                DateTimeOffset.UtcNow),
                cancellationToken);
        }

        _logger.LogInformation(
            "Handled GetReportQuery for {ReportId} (cache {CacheStatus})",
            request.Id,
            cacheMiss ? "miss" : "hit");

        return cached.Dto;
    }

    private sealed record CachedReport(Report Entity, ReportDto Dto);
}
