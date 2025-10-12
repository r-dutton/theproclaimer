using Azure.Messaging.ServiceBus;
using Sample.App.Messaging;

namespace Sample.Msg;

public class ServiceBusSenderFactory : IServiceBusSender
{
    private readonly List<ServiceBusMessage> _messages = new();

    public IReadOnlyList<ServiceBusMessage> Messages => _messages;

    public Task SendMessageAsync(ServiceBusMessage message, CancellationToken cancellationToken)
    {
        _messages.Add(message);
        return Task.CompletedTask;
    }
}
