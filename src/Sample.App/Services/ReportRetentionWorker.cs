using System;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sample.App.Repositories;
using Sample.App.Options;

namespace Sample.App.Services;

public sealed class ReportRetentionWorker : BackgroundService
{
    private readonly IReportRepository _repository;
    private readonly ILogger<ReportRetentionWorker> _logger;
    private readonly IOptions<ReportRetentionOptions> _options;

    public ReportRetentionWorker(IReportRepository repository, ILogger<ReportRetentionWorker> logger, IOptions<ReportRetentionOptions> options)
    {
        _repository = repository;
        _logger = logger;
        _options = options;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var retentionDays = Math.Max(1, _options.Value.RetentionDays);
            var cutoff = DateTimeOffset.UtcNow.AddDays(-retentionDays);
            var deleted = await _repository.PruneOldAsync(cutoff, stoppingToken);
            if (deleted > 0)
            {
                _logger.LogInformation("Pruned {Count} reports older than {Cutoff}", deleted, cutoff);
            }

            try
            {
                var interval = TimeSpan.FromMinutes(Math.Max(1, _options.Value.PollingIntervalMinutes));
                await Task.Delay(interval, stoppingToken);
            }
            catch (TaskCanceledException)
            {
                break;
            }
        }
    }
}
