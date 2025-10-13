using MediatR;
using Sample.App.Dtos;

namespace Sample.App.Queries;

public sealed record GetReportQuery(Guid Id) : IRequest<ReportDto>;
