using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sample.App.Commands;
using Sample.Data.Entities;
using Sample.Data.Infrastructure;

namespace Sample.App.Repositories;

public class ReportRepository : IReportRepository
{
    private readonly ReportsDbContext _dbContext;
    private readonly IMapper _mapper;

    public ReportRepository(ReportsDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
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
        return entity;
    }
}
