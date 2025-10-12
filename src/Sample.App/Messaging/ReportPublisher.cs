using Azure.Messaging.ServiceBus;

namespace Sample.App.Messaging;

public interface IServiceBusSender
{
    Task SendMessageAsync(ServiceBusMessage message, CancellationToken cancellationToken);
}

public class ReportPublisher
{
    private readonly IServiceBusSender _sender;

    public ReportPublisher(IServiceBusSender sender)
    {
        _sender = sender;
    }

    public Task PublishAsync(ReportPublished message, CancellationToken cancellationToken)
    {
        var envelope = new ServiceBusMessage(BinaryData.FromObjectAsJson(message));
        return _sender.SendMessageAsync(envelope, cancellationToken);
    }
}

public record ReportPublished(Guid ReportId, string Title, DateTimeOffset CreatedAt);
