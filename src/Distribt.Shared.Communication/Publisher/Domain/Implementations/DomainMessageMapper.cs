using Distribt.Shared.Communication.Messages;
using System.Reflection;

namespace Distribt.Shared.Communication.Publisher.Domain.Implementations
{
    public static class DomainMessageMapper
    {
        public static DomainMessage MapToMessage(object message, Metadata metadata)
        {
            if (message is IntegrationMessage)
                throw new ArgumentException("Message should not be of type DomainMessage, it should be a plain type");

            var buildWrapperMethodInfo = typeof(DomainMessageMapper).GetMethod(
                nameof(ToTypedIntegrationEvent),
                BindingFlags.Static | BindingFlags.NonPublic
            );

            var buildWrapperGenericMethodInfo = buildWrapperMethodInfo?.MakeGenericMethod([message.GetType()]);
            var wrapper = buildWrapperGenericMethodInfo?.Invoke(
                null,
                [
                    message,
                    metadata
                ]
            );
            return (wrapper as DomainMessage)!;
        }


        private static DomainMessage<T> ToTypedIntegrationEvent<T>(T message, Metadata metadata)
            => new(Guid.NewGuid().ToString(), typeof(T).Name, message, metadata);
    }
}
