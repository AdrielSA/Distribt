namespace Distribt.Shared.Communication.Consumer.Handler.Interfaces
{
    public interface IMessageHandlerRegistry
    {
        IEnumerable<IMessageHandler> GetMessageHandlerForType(Type messageHandlerType, Type messageType);
    }
}