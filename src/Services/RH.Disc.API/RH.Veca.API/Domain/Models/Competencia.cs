using RH.Core.DomainObjects;
using System;

namespace RH.Veca.API.Models
{
    public class Competencia : Entity
    {
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public int IntervaloMinimo { get; private set; }
        public int IntervaloMaximo { get; private set; }
        public Guid UsuarioId { get; set; }


        protected Competencia()
        {

        }

        public Competencia(string nome, string descricao, int intervaloMinimo, int intervaloMaximo, Guid usuarioId)
        {
            Nome = nome;
            Descricao = descricao;
            IntervaloMaximo = intervaloMaximo;
            IntervaloMinimo = intervaloMinimo;
            UsuarioId = UsuarioId;
        }
    }
}
