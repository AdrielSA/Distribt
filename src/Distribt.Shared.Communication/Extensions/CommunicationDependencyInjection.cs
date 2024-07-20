using Distribt.Shared.Communication.Consumer.Host;
using Distribt.Shared.Communication.Consumer.Manager.Implementations;
using Distribt.Shared.Communication.Consumer.Manager.Interfaces;
using Distribt.Shared.Communication.Messages;
using Distribt.Shared.Communication.Publisher.Domain.Implementations;
using Distribt.Shared.Communication.Publisher.Domain.Interfaces;
using Distribt.Shared.Communication.Publisher.Integration.Implementations;
using Distribt.Shared.Communication.Publisher.Integration.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Distribt.Shared.Communication.Extensions
{
    public static class CommunicationDependencyInjection
    {
        public static void AddConsumer<TMessage>(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IConsumerManager<TMessage>, ConsumerManager<TMessage>>();
            serviceCollection.AddSingleton<IHostedService, ConsumerHostedService<TMessage>>();
        }

        public static void AddPublisher<TMessage>(this IServiceCollection serviceCollection)
        {
            if (typeof(TMessage) == typeof(IntegrationMessage))
            {
                serviceCollection.AddIntegrationBusPublisher();
            }
            else if (typeof(TMessage) == typeof(DomainMessage))
            {
                serviceCollection.AddDomainBusPublisher();
            }
        }

        #region private methods

        private static void AddIntegrationBusPublisher(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IIntegrationMessagePublisher, DefaultIntegrationMessagePublisher>();
        }


        private static void AddDomainBusPublisher(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IDomainMessagePublisher, DefaultDomainMessagePublisher>();
        }

        #endregion
    }
}
