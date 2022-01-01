using RH.Core.DomainObjects;
using RH.Veca.API.Models;
using System.Collections.Generic;

namespace RH.Veca.API.Domain.Models
{
    public class Questionario : Entity, IAgregateRoot
    {
        public string Descricao { get; set; }
        public ICollection<Questao> Questoes { get; set; }
    }
}
