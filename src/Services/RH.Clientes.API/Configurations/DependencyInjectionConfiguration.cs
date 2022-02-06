using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RH.Clientes.API.Application.Commands.Registrar;
using RH.Clientes.API.Application.Events;
using RH.Clientes.API.Infra.Data;
using RH.Clientes.API.Infra.Data.Repository;
using RH.Clientes.API.Domain.Repository;
using RH.Core.Mediator;

namespace RH.Clientes.API.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<IRequestHandler<RegistrarClienteCommand, ValidationResult>, RegistrarClienteCommandHandler>();

            services.AddScoped<INotificationHandler<ClienteRegistradoEvent>, ClienteEventHandler>();

            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<ClientesContext>();
        }
    }
}
