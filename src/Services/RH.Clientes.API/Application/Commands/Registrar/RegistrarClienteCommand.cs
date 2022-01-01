using RH.Core.Messages;
using System;

namespace RH.Clientes.API.Application.Commands.Registrar
{
    public class RegistrarClienteCommand : Command
    {
        public Guid Id { get; private set; }
        public string NomeRazaoSocial { get; private set; }
        public string Email { get; private set; }

        public RegistrarClienteCommand(Guid id, string nomeRazaoSocial, string email)
        {
            AggregateId = id;
            Id = id;
            NomeRazaoSocial = nomeRazaoSocial;
            Email = email;
        }
    }
}
