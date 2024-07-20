﻿using Distribt.Shared.Communication.Consumer.Handler.Implementations;
using Distribt.Shared.Communication.Consumer.Handler.Interfaces;
using Distribt.Shared.Communication.Consumer;
using Distribt.Shared.Communication.Messages.Interfaces;
using Distribt.Shared.Communication.Publisher;
using Distribt.Shared.Communication.RabbitMQ.Consumer;
using Distribt.Shared.Communication.RabbitMQ.Publisher;
using Distribt.Shared.Communication.RabbitMQ.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Distribt.Shared.Communication.Extensions;

namespace Distribt.Shared.Communication.RabbitMQ.Extensions
{
    public static class RabbitMQDependencyInjection
    {
        public static void AddRabbitMQ(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.Configure<RabbitMQSettings>(configuration.GetSection("Bus:RabbitMQ"));
        }

        public static void AddConsumerHandlers(this IServiceCollection serviceCollection, IEnumerable<IMessageHandler> handlers)
        {

            serviceCollection.AddSingleton<IMessageHandlerRegistry>(new MessageHandlerRegistry(handlers));
            serviceCollection.AddSingleton<IHandleMessage, HandleMessage>();
        }

        public static void AddRabbitMqConsumer<TMessage>(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddConsumer<TMessage>();
            serviceCollection.AddSingleton<IMessageConsumer<TMessage>, RabbitMQMessageConsumer<TMessage>>();
        }

        public static void AddRabbitMQPublisher<TMessage>(this IServiceCollection serviceCollection)
            where TMessage : IMessage
        {
            serviceCollection.AddPublisher<TMessage>();
            serviceCollection.AddSingleton<IExternalMessagePublisher<TMessage>, RabbitMQMessagePublisher<TMessage>>();
        }
    }
}