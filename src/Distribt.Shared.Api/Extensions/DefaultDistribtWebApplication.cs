using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Distribt.Shared.Api.Extensions
{
    public static class DefaultDistribtWebApplication
    {
        public static WebApplication Create(Action<WebApplicationBuilder>? appBuilder = default)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            appBuilder?.Invoke(builder);

            return builder.Build();
        }

        public static void Run(WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
