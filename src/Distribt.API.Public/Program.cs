using Distribt.Shared.Api.Extensions;
using Distribt.Shared.Communication.Publisher.Integration.Interfaces;
using Distribt.Shared.Setup.Extensions;
using Distritb.Services.Subscriptions.Dtos.Records;

WebApplication app = DefaultDistribtWebApplication.Create(builder =>
{
    builder.Services.AddReverseProxy()
        .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));
    builder.Services.AddServiceBusIntegrationPublisher(builder.Configuration);
});

app.MapPost("/subscribe", async (SubscriptionDto subscriptionDto) =>
{
    IIntegrationMessagePublisher publisher = app.Services.GetRequiredService<IIntegrationMessagePublisher>();
    await publisher.Publish(subscriptionDto, routingKey: "subscription");
});

app.MapReverseProxy();

DefaultDistribtWebApplication.Run(app);