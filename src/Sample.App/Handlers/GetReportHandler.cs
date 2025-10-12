using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sample.App.Clients;
using Sample.App.Dtos;
using Sample.App.Messaging;
using Sample.App.Requests;
using Sample.Data.Db;
using Sample.Data.Entities;

namespace Sample.App.Handlers;

public class GetReportHandler : IRequestHandler<GetReportQuery, ReportDto>
{
    private readonly ReportsDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly ReportPublisher _publisher;
    private readonly ReportsClient _reportsClient;

    public GetReportHandler(ReportsDbContext dbContext, IMapper mapper, ReportPublisher publisher, ReportsClient reportsClient)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _publisher = publisher;
        _reportsClient = reportsClient;
    }

    public async Task<ReportDto> Handle(GetReportQuery request, CancellationToken cancellationToken)
    {
        var entity = await _dbContext.Reports
            .Where(r => r.Id == request.ReportId && r.TenantId == request.TenantId)
            .SingleAsync(cancellationToken);

        var dto = _mapper.Map<ReportDto>(entity);

        await _publisher.PublishAsync(new ReportPublished(entity.Id, entity.Title, entity.CreatedAt), cancellationToken);
        await _reportsClient.GetSummaryAsync(entity.Id, cancellationToken);

        return dto;
    }
}
