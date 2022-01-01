using RH.Core.DomainObjects;
using System.Collections;

namespace RH.Veca.API.Models
{
    public class Competencia : Entity
    {
        public string Descricao { get; set; }
        public IEnumerable NotaIdeal { get; set; }
    }
}
