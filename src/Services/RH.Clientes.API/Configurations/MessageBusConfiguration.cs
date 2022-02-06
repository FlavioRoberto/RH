using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RH.Clientes.API.Infra.Integration;
using RH.Core.Extensions;
using RH.MessageBus;

namespace RH.Clientes.API.Configurations
{
    public static class MessageBusConfiguration
    {
        public static void AddMessageBusConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"))
                    .AddHostedService<RegistroClienteintegrationHandler>();
        }
    }
}
