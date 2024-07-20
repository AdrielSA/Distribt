using Distribt.Shared.Communication.Messages;

namespace Distribt.Shared.Communication.Publisher.Domain.Interfaces
{
    public interface IDomainMessagePublisher
    {
        Task Publish(object message, Metadata? metadata = null, string? routingKey = null, CancellationToken cancellationToken = default);
        Task PublishMany(IEnumerable<object> messages, Metadata? metadata = null, string? routingKey = null, CancellationToken cancellationToken = default);
    }
}