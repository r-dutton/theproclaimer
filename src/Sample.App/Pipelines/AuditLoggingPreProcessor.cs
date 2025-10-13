using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sample.App.Options;

namespace Sample.App.Pipelines;

public sealed class AuditLoggingPreProcessor<TRequest> : IRequestPreProcessor<TRequest>
    where TRequest : notnull
{
    private readonly ILogger<AuditLoggingPreProcessor<TRequest>> _logger;
    private readonly IOptions<ReportRetentionOptions> _options;
    private readonly IMemoryCache _cache;

    public AuditLoggingPreProcessor(
        ILogger<AuditLoggingPreProcessor<TRequest>> logger,
        IOptions<ReportRetentionOptions> options,
        IMemoryCache cache)
    {
        _logger = logger;
        _options = options;
        _cache = cache;
    }

    public Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var retentionDays = Math.Max(1, _options.Value.RetentionDays);
        var cacheKey = $"audit::{typeof(TRequest).Name}";
        var expires = TimeSpan.FromDays(retentionDays);
        _cache.Set(cacheKey, DateTimeOffset.UtcNow, expires);
        _logger.LogInformation("Auditing request {RequestType} with retention window of {RetentionDays} days", typeof(TRequest).Name, retentionDays);
        return Task.CompletedTask;
    }
}
