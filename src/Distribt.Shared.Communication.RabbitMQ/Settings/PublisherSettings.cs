namespace Distribt.Shared.Communication.RabbitMQ.Settings
{
    public record PublisherSettings
    {
        public string? IntegrationExchange { get; init; }
        public string? DomainExchange { get; init; }
    }
}
