using Distribt.Shared.Api.Extensions;
using Distribt.Shared.Communication.Consumer.Handler.Interfaces;
using Distritb.Services.Subscriptions.Consumer.Handlers;
using Distribt.Shared.Setup.Extensions;

WebApplication app = DefaultDistribtWebApplication.Create(x =>
{
    x.Services.AddHandlers(
    [
        new SubscriptionHandler(),
        new UnSubscriptionHandler()
    ]);
    x.Services.AddServiceBusIntegrationConsumer(x.Configuration);
});


DefaultDistribtWebApplication.Run(app);
