using Distribt.Shared.Communication.Consumer.Handler.Interfaces;
using Distribt.Shared.Communication.Messages.Interfaces;
using Distribt.Shared.Serialization.Interfaces;
using RabbitMQ.Client;

namespace Distribt.Shared.Communication.RabbitMQ.Consumer
{
    public class RabbitMQMessageReceiver(
        IModel channel, ISerializer serializer, IHandleMessage handleMessage) : DefaultBasicConsumer
    {
        private readonly IModel _channel = channel;
        private readonly ISerializer _serializer = serializer;
        private readonly IHandleMessage _handleMessage = handleMessage;

        private byte[]? MessageBody { get; set; }
        private Type? MessageType { get; set; }
        private ulong DeliveryTag { get; set; }

        public override void HandleBasicDeliver(string consumerTag, ulong deliveryTag, bool redelivered, string exchange,
        string routingKey, IBasicProperties properties, ReadOnlyMemory<byte> body)
        {
            MessageType = Type.GetType(properties.Type)!;
            MessageBody = body.ToArray();
            DeliveryTag = deliveryTag; // Used to delete the message from rabbitMQ

            // #5 not ideal solution, but seems that this HandleBasicDeliver needs to be like this as its not async
            var t = Task.Run(HandleMessage);
            t.Wait();
        }

        private async Task HandleMessage()
        {
            if (MessageBody == null || MessageType == null)
            {
                throw new ArgumentException("Neither the body or the messageType has been populated");
            }

            IMessage message = (_serializer.DeserializeObject(MessageBody, MessageType) as IMessage)
                               ?? throw new ArgumentException("The message did not deserialized properly");

            await _handleMessage.Handle(message, CancellationToken.None);

            _channel.BasicAck(DeliveryTag, false);
        }
    }
}
