using Distribt.Shared.Communication.Consumer.Handler.Interfaces;
using Distribt.Shared.Communication.Messages.Interfaces;
using System.Reflection;

namespace Distribt.Shared.Communication.Consumer.Handler.Implementations
{
    public class HandleMessage(IMessageHandlerRegistry messageHandlerRegistry) : IHandleMessage
    {
        private readonly IMessageHandlerRegistry _messageHandlerRegistry = messageHandlerRegistry;

        public Task Handle(IMessage message, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(message);

            Type messageType = message.GetType();
            var handlerType = typeof(IMessageHandler<>).MakeGenericType(messageType);
            List<IMessageHandler> handlers = _messageHandlerRegistry.GetMessageHandlerForType(handlerType, messageType).ToList();

            foreach (IMessageHandler handler in handlers)
            {
                Type messageHandlerType = handler.GetType();

                MethodInfo? handle = messageHandlerType.GetMethods()
                    .Where(methodInfo => methodInfo.Name == nameof(IMessageHandler<object>.Handle))
                    .FirstOrDefault(info => info.GetParameters()
                    .Select(parameter => parameter.ParameterType)
                    .Contains(message.GetType()));

                if (handle != null)
                    return (Task)handle.Invoke(handler, [message, cancellationToken])!;
            }
            return Task.CompletedTask;
        }
    }
}
