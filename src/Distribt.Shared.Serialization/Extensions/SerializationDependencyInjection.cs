using Distribt.Shared.Serialization.Implementations;
using Distribt.Shared.Serialization.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Distribt.Shared.Serialization.Extensions
{
    public static class SerializationDependencyInjection
    {
        public static void AddSerializer(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ISerializer, Serializer>();
        }
    }
}
