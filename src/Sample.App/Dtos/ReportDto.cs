namespace Sample.App.Dtos;

public record ReportDto(Guid Id, string Title, string Body, string Summary, DateTimeOffset CreatedAt, string Classification);
