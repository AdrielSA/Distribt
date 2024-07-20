using Distribt.Shared.Communication.Messages;
using Distribt.Shared.Communication.Publisher.Integration.Interfaces;

namespace Distribt.Shared.Communication.Publisher.Integration.Implementations
{
    public class DefaultIntegrationMessagePublisher(IExternalMessagePublisher<IntegrationMessage> externalPublisher) : IIntegrationMessagePublisher
    {
        private readonly IExternalMessagePublisher<IntegrationMessage> _externalPublisher = externalPublisher;

        public Task Publish(object message, Metadata? metadata = null,
            string? routingKey = null, CancellationToken cancellationToken = default)
        {
            Metadata calculatedMetadata = CalculateMetadata(metadata);
            var integrationMessage = IntegrationMessageMapper.MapToMessage(message, calculatedMetadata);
            return _externalPublisher.Publish(integrationMessage, routingKey, cancellationToken);
        }

        public Task PublishMany(IEnumerable<object> messages, Metadata? metadata = null,
            string? routingKey = null, CancellationToken cancellationToken = default)
        {
            var integrationMessages =
                messages.Select(a => IntegrationMessageMapper.MapToMessage(a, CalculateMetadata(metadata)));
            return _externalPublisher.PublishMany(integrationMessages, routingKey, cancellationToken);
        }

        private Metadata CalculateMetadata(Metadata? metadata)
            => metadata ?? new Metadata(Guid.NewGuid().ToString(), DateTime.UtcNow);
    }
}
