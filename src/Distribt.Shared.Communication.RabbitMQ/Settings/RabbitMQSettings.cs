namespace Distribt.Shared.Communication.RabbitMQ.Settings
{
    public record RabbitMQSettings
    {
        public string Hostname { get; init; } = null!;
        public string Username { get; init; } = null!;
        public string Password { get; init; } = null!;
        public PublisherSettings? Publisher { get; init; }
        public ConsumerSettings? Consumer { get; init; }
    }
}
