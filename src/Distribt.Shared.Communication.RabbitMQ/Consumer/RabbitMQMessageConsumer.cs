using Distribt.Shared.Communication.Consumer;
using Distribt.Shared.Communication.Consumer.Handler.Interfaces;
using Distribt.Shared.Communication.Messages;
using Distribt.Shared.Communication.RabbitMQ.Settings;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using ISerializer = Distribt.Shared.Serialization.Interfaces.ISerializer;

namespace Distribt.Shared.Communication.RabbitMQ.Consumer
{
    public class RabbitMQMessageConsumer<TMessage>(ISerializer serializer, 
        IOptions<RabbitMQSettings> settings, IHandleMessage handleMessage) : IMessageConsumer<TMessage>
    {
        private readonly ISerializer _serializer = serializer;
        private readonly IHandleMessage _handleMessage = handleMessage;
        private readonly RabbitMQSettings _settings = settings.Value;
        private readonly ConnectionFactory _connectionFactory = new() 
        {
            HostName = settings.Value.Hostname,
            Password = settings.Value.Password,
            UserName = settings.Value.Username
        };

        public Task StartAsync(CancellationToken cancelToken = default)
            => Task.Run(async () => await Consume(), cancelToken);


        #region private methods

        private Task Consume()
        {
            IConnection connection = _connectionFactory.CreateConnection(); // #6 using (implement it correctly)
            IModel channel = connection.CreateModel(); // #6 using (implement it correctly)
            RabbitMQMessageReceiver receiver = new(channel, _serializer, _handleMessage);
            string queue = GetCorrectQueue();

            channel.BasicConsume(queue, false, receiver);

            // #5 this should be here await consumer.HandleMessage();
            return Task.CompletedTask;
        }

        private string GetCorrectQueue()
        {
            return (typeof(TMessage) == typeof(IntegrationMessage)
                       ? _settings.Consumer?.IntegrationQueue
                       : _settings.Consumer?.DomainQueue)
                   ?? throw new ArgumentException("please configure the queues on the appsettings");
        }

        #endregion
    }
}
