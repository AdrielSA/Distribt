using Distribt.Shared.Communication.Messages.Interfaces;

namespace Distribt.Shared.Communication.Consumer.Handler.Interfaces
{
    public interface IHandleMessage
    {
        Task Handle(IMessage message, CancellationToken cancellationToken = default);
    }
}