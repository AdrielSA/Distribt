namespace Distribt.Shared.Communication.RabbitMQ.Settings
{
    public record ConsumerSettings
    {
        public string? IntegrationQueue { get; init; }
        public string? DomainQueue { get; init; }
    }
}
