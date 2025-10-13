
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

var reports = new List<ReportSummary>
{
    new(new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), "Quarterly Report", DateTimeOffset.UtcNow.AddDays(-1), "tenant-001"),
    new(Guid.NewGuid(), "Annual Overview", DateTimeOffset.UtcNow.AddDays(-30), "tenant-001"),
    new(Guid.NewGuid(), "Security Audit", DateTimeOffset.UtcNow.AddDays(-5), "tenant-002")
};

app.MapGet("/api/reports/{id:guid}", (Guid id) =>
{
    var report = reports.FirstOrDefault(r => r.Id == id);
    return report is null ? Results.NotFound() : Results.Ok(report);
}).WithName("GetReportSummary");

app.MapPost("/api/reports/search", (SearchRequest request) =>
{
    var matches = reports.Where(r => r.TenantId.Equals(request.TenantId, StringComparison.OrdinalIgnoreCase)).ToList();
    return Results.Ok(matches);
}).WithName("SearchReports");

app.Run();

record ReportSummary(Guid Id, string Title, DateTimeOffset CreatedAt, string TenantId);
record SearchRequest(string TenantId);
