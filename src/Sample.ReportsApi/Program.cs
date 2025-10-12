var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/api/reports/{id:guid}/summary", (Guid id) => Results.Ok(new
    {
        ReportId = id,
        Summary = "Report summary placeholder"
    }))
    .WithName("GetReportSummary")
    .WithOpenApi();

app.MapPost("/api/reports/search", (ReportSearch search) =>
    Results.Ok(new[]
    {
        new ReportSearchResult(search.Query ?? "none", DateTimeOffset.UtcNow)
    }))
    .WithName("SearchReports")
    .WithOpenApi();

app.Run();

public record ReportSearch(string? Query);

public record ReportSearchResult(string Query, DateTimeOffset ProcessedAt);
