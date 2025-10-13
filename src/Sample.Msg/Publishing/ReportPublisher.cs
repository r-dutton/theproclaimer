using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Logging;
using Sample.Msg.Contracts;

namespace Sample.Msg.Publishing;

public interface IReportPublisher
{
    Task PublishAsync(ReportPublished message, CancellationToken cancellationToken = default);
}

public class ReportPublisher : IReportPublisher, IAsyncDisposable
{
    private readonly ServiceBusSender _sender;
    private readonly ILogger<ReportPublisher> _logger;

    public ReportPublisher(ServiceBusClient client, ILogger<ReportPublisher> logger)
    {
        _sender = client.CreateSender("reports.events");
        _logger = logger;
    }

    public async Task PublishAsync(ReportPublished message, CancellationToken cancellationToken = default)
    {
        var envelope = new ServiceBusMessage(BinaryData.FromObjectAsJson(message))
        {
            Subject = "reports.published",
            CorrelationId = message.ReportId.ToString()
        };

        await _sender.SendMessageAsync(envelope, cancellationToken);
        _logger.LogInformation("Published report {ReportId} to Service Bus", message.ReportId);
    }

    public async ValueTask DisposeAsync()
    {
        await _sender.DisposeAsync();
    }
}
