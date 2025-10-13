using System;
using System.Threading;
using System.Threading.Tasks;
using Sample.App.Commands;
using Sample.Data.Entities;

namespace Sample.App.Repositories;

public interface IReportRepository
{
    Task<Report> CreateDetailedAsync(CreateDetailedReportCommand command, CancellationToken cancellationToken);
    Task<int> PruneOldAsync(DateTimeOffset cutoff, CancellationToken cancellationToken);
}
