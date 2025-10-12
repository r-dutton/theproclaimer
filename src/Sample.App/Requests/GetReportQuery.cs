using MediatR;
using Sample.App.Dtos;

namespace Sample.App.Requests;

public record GetReportQuery(Guid ReportId, Guid TenantId) : IRequest<ReportDto>;
