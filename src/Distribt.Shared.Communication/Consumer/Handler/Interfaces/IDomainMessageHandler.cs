using Distribt.Shared.Communication.Messages;

namespace Distribt.Shared.Communication.Consumer.Handler.Interfaces
{
    public interface IDomainMessageHandler : IMessageHandler
    {
    }

    public interface IDomainMessageHandler<TMessage>
        : IMessageHandler<IntegrationMessage<TMessage>>, IDomainMessageHandler
    {
    }
}
