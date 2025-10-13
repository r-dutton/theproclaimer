using MediatR;
using Sample.App.Dtos;

namespace Sample.App.Commands;

public record CreateReportCommand(string Title, string TenantId) : IRequest<ReportDto>;

public record CreateDetailedReportCommand(string Title, string TenantId, string Summary) : CreateReportCommand(Title, TenantId);
