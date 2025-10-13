using AutoMapper;
using MediatR;
using Sample.App.Commands;
using Sample.App.Dtos;
using Sample.App.Repositories;
using Sample.Msg.Contracts;
using Sample.Msg.Publishing;

namespace Sample.App.Handlers;

public class CreateDetailedReportHandler : IRequestHandler<CreateDetailedReportCommand, ReportDto>
{
    private readonly IReportRepository _repository;
    private readonly IMapper _mapper;
    private readonly IReportPublisher _publisher;

    public CreateDetailedReportHandler(IReportRepository repository, IMapper mapper, IReportPublisher publisher)
    {
        _repository = repository;
        _mapper = mapper;
        _publisher = publisher;
    }

    public async Task<ReportDto> Handle(CreateDetailedReportCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.CreateDetailedAsync(request, cancellationToken);
        await _publisher.PublishAsync(new ReportPublished(entity.Id, entity.TenantId, entity.Title, entity.CreatedAt), cancellationToken);
        return _mapper.Map<ReportDto>(entity);
    }
}
