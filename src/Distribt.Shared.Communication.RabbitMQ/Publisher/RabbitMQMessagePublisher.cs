using Distribt.Shared.Communication.Messages;
using Distribt.Shared.Communication.Messages.Interfaces;
using Distribt.Shared.Communication.Publisher;
using Distribt.Shared.Communication.RabbitMQ.Settings;
using Distribt.Shared.Serialization.Interfaces;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;

namespace Distribt.Shared.Communication.RabbitMQ.Publisher
{
    public class RabbitMQMessagePublisher<TMessage>(
        ISerializer serializer, IOptions<RabbitMQSettings> settings) 
        : IExternalMessagePublisher<TMessage> where TMessage : IMessage
    {
        private readonly ISerializer _serializer = serializer;
        private readonly RabbitMQSettings _settings = settings.Value;
        private readonly ConnectionFactory _connectionFactory = new()
        {
            HostName = settings.Value.Hostname,
            Password = settings.Value.Password,
            UserName = settings.Value.Username
        };


        public Task Publish(TMessage message, 
            string? routingKey = null, CancellationToken cancellationToken = default)
        {
            using IConnection connection = _connectionFactory.CreateConnection();
            using IModel model = connection.CreateModel();

            PublishSingle(message, model, routingKey);

            return Task.CompletedTask;
        }

        public Task PublishMany(IEnumerable<TMessage> messages, 
            string? routingKey = null, CancellationToken cancellationToken = default)
        {
            using IConnection connection = _connectionFactory.CreateConnection();
            using IModel model = connection.CreateModel();
            foreach (TMessage message in messages)
            {
                PublishSingle(message, model, routingKey);
            }
            return Task.CompletedTask;
        }


        #region private methods

        private void PublishSingle(TMessage message, IModel model, string? routingKey)
        {
            var properties = model.CreateBasicProperties();
            properties.Persistent = true;
            properties.Type = RemoveVersion(message.GetType());

            model.BasicPublish(exchange: GetCorrectExchange(),
                routingKey: routingKey ?? string.Empty,
                basicProperties: properties,
                body: _serializer.SerializeObjectToByteArray(message));
        }

        private string GetCorrectExchange()
        {
            return (typeof(TMessage) == typeof(IntegrationMessage)
                ? _settings.Publisher?.IntegrationExchange
                : _settings.Publisher?.DomainExchange)
                   ?? throw new ArgumentException("please configure the Exchanges on the appsettings");
        }

        private static string RemoveVersion(Type type)
            => RemoveVersionFromQualifiedName(type.AssemblyQualifiedName ?? string.Empty, 0);

        private static string RemoveVersionFromQualifiedName(string assemblyQualifiedName, int indexStart)
        {
            var stringBuilder = new StringBuilder();
            var indexOfGenericClose = assemblyQualifiedName.IndexOf("]]", indexStart + 1, StringComparison.Ordinal);
            var indexOfVersion = assemblyQualifiedName.IndexOf(", Version", indexStart + 1, StringComparison.Ordinal);

            if (indexOfVersion < 0)
                return assemblyQualifiedName;

            stringBuilder.Append(assemblyQualifiedName.AsSpan(indexStart, indexOfVersion - indexStart));

            if (indexOfGenericClose > 0)
                stringBuilder.Append(RemoveVersionFromQualifiedName(assemblyQualifiedName, indexOfGenericClose));

            return stringBuilder.ToString();
        }

        #endregion
    }
}
