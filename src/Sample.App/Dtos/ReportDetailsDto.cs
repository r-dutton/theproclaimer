namespace Sample.App.Dtos;

public record ReportDetailsDto(Guid Id, string Summary, DateTimeOffset LastSynced);
