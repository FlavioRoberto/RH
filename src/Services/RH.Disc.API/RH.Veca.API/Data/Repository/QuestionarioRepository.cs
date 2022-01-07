using Microsoft.EntityFrameworkCore;
using RH.Core.Data;
using RH.Veca.API.Domain.Models;
using RH.Veca.API.Domain.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RH.Veca.API.Data.Repository
{
    public class QuestionarioRepository : IQuestionarioRepositorio
    {
        private readonly VecaContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public QuestionarioRepository(VecaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Questionario>> ObterTodos()
        {
            return await _context.Questionario.AsNoTracking().ToListAsync();
        }

        public void Adicionar(Questionario questionario)
        {
            _context.Questionario.Add(questionario);
        }

        public void Atualizar(Questionario questionario)
        {
            _context.Questionario.Update(questionario);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
