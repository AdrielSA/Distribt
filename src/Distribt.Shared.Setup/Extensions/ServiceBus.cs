using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Distribt.Shared.Communication.RabbitMQ.Extensions;
using Distribt.Shared.Communication.Messages;
using Distribt.Shared.Communication.Consumer.Handler.Interfaces;

namespace Distribt.Shared.Setup.Extensions
{
    public static class ServiceBus
    {
        public static void AddServiceBusIntegrationPublisher(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddRabbitMQ(configuration);
            serviceCollection.AddRabbitMQPublisher<IntegrationMessage>();
        }

        public static void AddServiceBusIntegrationConsumer(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddRabbitMQ(configuration);
            serviceCollection.AddRabbitMqConsumer<IntegrationMessage>();
        }

        public static void AddServiceBusDomainPublisher(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddRabbitMQ(configuration);
            serviceCollection.AddRabbitMQPublisher<DomainMessage>();
        }

        public static void AddServiceBusDomainConsumer(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddRabbitMQ(configuration);
            serviceCollection.AddRabbitMqConsumer<DomainMessage>();
        }

        public static void AddHandlers(this IServiceCollection serviceCollection, IEnumerable<IMessageHandler> handlers)
        {
            serviceCollection.AddConsumerHandlers(handlers);
        }
    }
}
