using RH.Core.DomainObjects;
using System;

namespace RH.Clientes.API.Domain.Models
{
    public class Cliente : Entity, IAgregateRoot
    {
        public string NomeRazaoSocial { get; private set; }
        public Email Email { get; private set; }

        protected Cliente() { }

        public Cliente(Guid id, string nomeRazaoSocial, string email)
        {
            Id = id;
            NomeRazaoSocial = nomeRazaoSocial;
            Email = new Email(email);
        }
    }
}
