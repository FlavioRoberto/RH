using RH.Core.DomainObjects;
using RH.Core.DomainObjects.Data;
using RH.Veca.API.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RH.Veca.API.Domain.Repository
{
    public interface IQuestionarioRepositorio : IRepository<Questionario>
    {
        Task<IEnumerable<Questionario>> ObterTodos(); 
        
        void Adicionar(Questionario questionario);
        void Atualizar(Questionario questionario);
    }
}
