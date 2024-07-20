namespace Distribt.Shared.Communication.Consumer.Handler.Interfaces
{
    public interface IMessageHandler
    {
    }

    public interface IMessageHandler<in TMessage> : IMessageHandler
    {
        Task Handle(TMessage message, CancellationToken cancelToken = default);
    }
}
