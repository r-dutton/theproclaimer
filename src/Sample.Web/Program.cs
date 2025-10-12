using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Sample.App.Clients;
using Sample.App.Handlers;
using Sample.App.Mapping;
using Sample.App.Messaging;
using Sample.App.Requests;
using Sample.App.Validation;
using Sample.Data.Db;
using Sample.Msg;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ReportsDbContext>(options =>
    options.UseInMemoryDatabase("reports"));

builder.Services.AddMediatR(typeof(GetReportHandler).Assembly);
builder.Services.AddAutoMapper(typeof(ReportProfile));
builder.Services.AddTransient<GetReportQueryValidator>();
builder.Services.AddSingleton<IServiceBusSender, ServiceBusSenderFactory>();
builder.Services.AddScoped<ReportPublisher>();

builder.Services.AddHttpClient<ReportsClient>(client =>
{
    var baseUrl = builder.Configuration["Services:ReportsApi:BaseUrl"] ?? "https://reports-api.local";
    client.BaseAddress = new Uri(baseUrl);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.MapGet("/health", () => Results.Ok(new { Status = "ok" }))
    .WithName("HealthCheck")
    .WithTags("diagnostics");

app.Run();
