using Distribt.Shared.Communication.Messages;

namespace Distribt.Shared.Communication.Consumer.Handler.Interfaces
{
    public interface IIntegrationMessageHandler : IMessageHandler
    {
    }

    public interface IIntegrationMessageHandler<TMessage>
        : IMessageHandler<IntegrationMessage<TMessage>>, IIntegrationMessageHandler
    {
    }
}
