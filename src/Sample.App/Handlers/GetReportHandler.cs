using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sample.App.Dtos;
using Sample.App.Queries;
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

    public GetReportHandler(
        ReportsDbContext dbContext,
        IMapper mapper,
        IReportPublisher publisher,
        ILogger<GetReportHandler> logger)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _publisher = publisher;
        _logger = logger;
    }

    public async Task<ReportDto> Handle(GetReportQuery request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Reports
            .AsNoTracking()
            .FirstAsync(r => r.Id == request.Id, cancellationToken);

        var dto = _mapper.Map<ReportDto>(entity);

        await _publisher.PublishAsync(new ReportPublished(
            entity.Id,
            entity.TenantId,
            entity.Title,
            DateTimeOffset.UtcNow),
            cancellationToken);

        _logger.LogInformation("Handled GetReportQuery for {ReportId}", request.Id);

        return dto;
    }
}
