using MediatR;
using RH.Core.DomainObjects;
using RH.Core.Messages;
using System;

namespace RH.Clientes.API.Application.Events
{
    public class ClienteRegistradoEvent : Event
    {
        public Guid Id { get; private set; }
        public string NomeRazaoSocial { get; private set; }
        public Email Email { get; private set; }

        public ClienteRegistradoEvent(Guid id, string nomeRazaoSocial, string email)
        {
            Id = id;
            NomeRazaoSocial = nomeRazaoSocial;
            Email = new Email(email);
        }
    }
}
