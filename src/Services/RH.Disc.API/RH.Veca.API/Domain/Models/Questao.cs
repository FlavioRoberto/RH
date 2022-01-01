using RH.Core.DomainObjects;

namespace RH.Veca.API.Models
{
    public class Questao : Entity
    {
        public string Descricao { get; set; }
        public Frase OpcaoA { get; set; }
        public Frase OpcaoB { get; set; }
    }
}
