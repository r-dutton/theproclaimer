using AutoMapper;
using MediatR;
using Sample.App.Commands;
using Sample.App.Dtos;
using Sample.App.Notifications;
using Sample.App.Repositories;
using Sample.Msg.Contracts;
using Sample.Msg.Publishing;

namespace Sample.App.Handlers;

public class CreateDetailedReportHandler : IRequestHandler<CreateDetailedReportCommand, ReportDto>
{
    private readonly IMediator _mediator;
    private readonly IReportRepository _repository;
    private readonly IMapper _mapper;
    private readonly IReportPublisher _publisher;

    public CreateDetailedReportHandler(IMediator mediator, IReportRepository repository, IMapper mapper, IReportPublisher publisher)
    {
        _mediator = mediator;
        _repository = repository;
        _mapper = mapper;
        _publisher = publisher;
    }

    public async Task<ReportDto> Handle(CreateDetailedReportCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.CreateDetailedAsync(request, cancellationToken);
        await _publisher.PublishAsync(new ReportPublished(entity.Id, entity.TenantId, entity.Title, entity.CreatedAt), cancellationToken);
        await _mediator.Publish(new ReportGeneratedNotification(entity.Id, entity.TenantId, entity.Title, entity.CreatedAt), cancellationToken);
        return _mapper.Map<ReportDto>(entity);
    }
}
