using Microsoft.EntityFrameworkCore;
using RH.Clientes.API.Domain.Models;
using RH.Clientes.API.Domain.Repository;
using RH.Core.Data;
using System.Threading.Tasks;

namespace RH.Clientes.API.Data.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly ClientesContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public ClienteRepository(ClientesContext context)
        {
            _context = context;
        }

        public void Adicionar(Cliente cliente)
        {
            _context.Cliente.Add(cliente);
        }

        public Task<Cliente> ObterPorEmail(string email)
        {
            return _context.Cliente.AsNoTracking().FirstOrDefaultAsync(x => x.Email.Endereco == email);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
