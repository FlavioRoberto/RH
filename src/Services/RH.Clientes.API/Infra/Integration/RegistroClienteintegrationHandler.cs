using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RH.Clientes.API.Application.Commands.Registrar;
using RH.Core.Mediator;
using RH.Core.Messages.Integrations;
using RH.MessageBus;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RH.Clientes.API.Infra.Integration
{
    public class RegistroClienteintegrationHandler : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public RegistroClienteintegrationHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var bus = scope.ServiceProvider.GetRequiredService<IMessageBus>();
                bus.RespondAsync<UsuarioRegistradoIntegrationEvent, ResponseIntegrationMessage>(async request => new ResponseIntegrationMessage(await RegistrarCliente(request)));
            }

            return Task.CompletedTask;
        }

        private async Task<ValidationResult> RegistrarCliente(UsuarioRegistradoIntegrationEvent message)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
                var clienteCommand = new RegistrarClienteCommand(message.Id, message.NomeRazaoSocial, message.Email);
                return await mediator.EnviarComando(clienteCommand);
            }
        }
    }
}
