using Distribt.Shared.Communication.Consumer.Handler.Interfaces;
using Distribt.Shared.Communication.Messages;
using Distritb.Services.Subscriptions.Dtos.Records;

namespace Distritb.Services.Subscriptions.Consumer.Handlers
{
    public class SubscriptionHandler : IIntegrationMessageHandler<SubscriptionDto>
    {
        public Task Handle(IntegrationMessage<SubscriptionDto> message, CancellationToken cancelToken = default)
        {
            Console.WriteLine($"Email {message.Content.Email} successfully subscribed.");
            return Task.CompletedTask;
        }
    }
}
