using RH.Core.DomainObjects;
using System;

namespace RH.Core.Messages.Integrations
{
    public class UsuarioRegistradoIntegrationEvent : IntegrationEvent
    {
        public Guid Id { get; private set; }
        public string NomeRazaoSocial { get; private set; }
        public string Email { get; private set; }

        public UsuarioRegistradoIntegrationEvent(string id, string nomeRazaoSocial, string email)
        {
            Id = Guid.Parse(id);
            NomeRazaoSocial = nomeRazaoSocial;
            Email = email;
        }
    }
}
