using FluentValidation.Results;
using MediatR;
using RH.Clientes.API.Application.Events;
using RH.Clientes.API.Domain.Models;
using RH.Clientes.API.Domain.Repository;
using RH.Core.Messages;
using System.Threading;
using System.Threading.Tasks;

namespace RH.Clientes.API.Application.Commands.Registrar
{
    public class RegistrarClienteCommandHandler : CommandHandler, IRequestHandler<RegistrarClienteCommand, ValidationResult>
    {
        private readonly IClienteRepository _clienteRepository;

        public RegistrarClienteCommandHandler(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<ValidationResult> Handle(RegistrarClienteCommand request, CancellationToken cancellationToken)
        {
            if (!request.EhValido())
                return request.ValidationResult;

            var cliente = new Cliente(request.Id, request.NomeRazaoSocial, request.Email);

            var clienteExistente = await _clienteRepository.ObterPorEmail(request.Email);

            if (clienteExistente != null)
            {
                AdicionarErro("O Cpf informado já está em uso!");
                return ValidationResult;
            }

            _clienteRepository.Adicionar(cliente);

            cliente.AdicionarEvento(new ClienteRegistradoEvent(request.Id, request.NomeRazaoSocial, request.Email));

            return await PersistirDados(_clienteRepository.UnitOfWork);
        }
    }
}
