using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace RH.Clientes.API.Application.Events
{
    public class ClienteEventHandler : INotificationHandler<ClienteRegistradoEvent>
    {
        public Task Handle(ClienteRegistradoEvent notification, CancellationToken cancellationToken)
        {
            //Enviar e-mail etc...
            return Task.CompletedTask;
        }
    }
}
