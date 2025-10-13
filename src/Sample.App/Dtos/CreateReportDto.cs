namespace Sample.App.Dtos;

public record CreateReportDto(string Title, string TenantId);

public record CreateDetailedReportDto(string Title, string TenantId, string Summary) : CreateReportDto(Title, TenantId);
