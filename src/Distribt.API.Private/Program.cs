using Distribt.Shared.Api.Extensions;

WebApplication app = DefaultDistribtWebApplication.Create(builder =>
{
    builder.Services.AddReverseProxy()
        .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));
});

app.MapReverseProxy();
DefaultDistribtWebApplication.Run(app);