using FluentValidation.Results;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace RH.Clientes.API.Application.Commands.Registrar
{
    public class RegistrarClienteCommandHandler : IRequestHandler<RegistrarClienteCommand, ValidationResult>
    {
        public Task<ValidationResult> Handle(RegistrarClienteCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
