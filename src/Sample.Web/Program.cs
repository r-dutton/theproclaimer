
using Azure.Messaging.ServiceBus;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sample.App.Handlers;
using Sample.App.Mapping;
using Sample.App.Repositories;
using Sample.App.Services;
using Sample.App.Validation;
using Sample.App.Options;
using Sample.Data.Infrastructure;
using Sample.Msg.Publishing;
using Sample.Web.HttpClients;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(typeof(GetReportHandler));
builder.Services.AddAutoMapper(typeof(ReportProfile).Assembly);
builder.Services.AddValidatorsFromAssemblyContaining<ReportDtoValidator>();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.Configure<ReportRetentionOptions>(builder.Configuration.GetSection(ReportRetentionOptions.SectionName));

var connectionString = builder.Configuration.GetConnectionString("Reports") ?? "Data Source=reports.db";
builder.Services.AddDbContext<ReportsDbContext>(options => options.UseSqlite(connectionString));

builder.Services.AddSingleton(_ => new ServiceBusClient(builder.Configuration.GetValue<string>("ServiceBus:ConnectionString") ?? "Endpoint=sb://local/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA="));
builder.Services.AddScoped<IReportPublisher, ReportPublisher>();
builder.Services.AddScoped<IReportRepository, ReportRepository>();
builder.Services.AddScoped<IReportAuditRepository, ReportAuditRepository>();
builder.Services.AddHostedService<ReportRetentionWorker>();

builder.Services.AddHttpClient<ReportsClient>((sp, client) =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    var baseUrl = config.GetValue<string>("Downstream:Reports:BaseUrl") ?? "https://localhost:6001";
    client.BaseAddress = new Uri(baseUrl);
});

builder.Services.AddMemoryCache();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ReportsDbContext>();
    await db.Database.EnsureCreatedAsync();
    await SeedData.EnsureSeededAsync(db);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.MapGet("/internal/reports/{id:guid}", async (Guid id, IMediator mediator, CancellationToken cancellationToken) =>
{
    var dto = await mediator.Send(new Sample.App.Queries.GetReportQuery(id), cancellationToken);
    return Results.Ok(dto);
}).WithName("GetInternalReport");

app.Run();
