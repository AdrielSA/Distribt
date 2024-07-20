using Distribt.Shared.Communication.Messages;
using Distribt.Shared.Communication.Publisher.Domain.Interfaces;

namespace Distribt.Shared.Communication.Publisher.Domain.Implementations
{
    public class DefaultDomainMessagePublisher(IExternalMessagePublisher<DomainMessage> externalPublisher) : IDomainMessagePublisher
    {
        private readonly IExternalMessagePublisher<DomainMessage> _externalPublisher = externalPublisher;

        public Task Publish(object message, Metadata? metadata = null,
            string? routingKey = null, CancellationToken cancellationToken = default)
        {
            Metadata calculatedMetadata = CalculateMetadata(metadata);
            var domainMessage = DomainMessageMapper.MapToMessage(message, calculatedMetadata);
            return _externalPublisher.Publish(domainMessage, routingKey, cancellationToken);
        }

        public Task PublishMany(IEnumerable<object> messages, Metadata? metadata = null,
            string? routingKey = null, CancellationToken cancellationToken = default)
        {
            var domainMessages =
                messages.Select(a => DomainMessageMapper.MapToMessage(a, CalculateMetadata(metadata)));
            return _externalPublisher.PublishMany(domainMessages, routingKey, cancellationToken);
        }

        private static Metadata CalculateMetadata(Metadata? metadata)
            => metadata ?? new Metadata(Guid.NewGuid().ToString(), DateTime.UtcNow);
    }
}
