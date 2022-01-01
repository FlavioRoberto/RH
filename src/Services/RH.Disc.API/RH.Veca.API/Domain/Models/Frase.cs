using RH.Core.DomainObjects;

namespace RH.Veca.API.Models
{
    public class Frase : Entity
    {
        public string Descricao { get; set; }
        public Competencia Competencia { get; set; }
    }
}
