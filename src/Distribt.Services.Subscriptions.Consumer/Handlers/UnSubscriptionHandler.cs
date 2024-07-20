using Distribt.Shared.Communication.Consumer.Handler.Interfaces;
using Distribt.Shared.Communication.Messages;
using Distritb.Services.Subscriptions.Dtos.Records;

namespace Distritb.Services.Subscriptions.Consumer.Handlers
{
    public class UnSubscriptionHandler : IIntegrationMessageHandler<UnSubscriptionDto>
    {
        public Task Handle(IntegrationMessage<UnSubscriptionDto> message, CancellationToken cancelToken = default)
        {
            Console.WriteLine($"the email {message.Content.Email} has unsubscribed.");
            //TODO: Full use case
            return Task.CompletedTask;
        }
    }
}
