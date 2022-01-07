using RH.Clientes.API.Domain.Models;
using RH.Core.DomainObjects.Data;
using System.Threading.Tasks;

namespace RH.Clientes.API.Domain.Repository
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        void Adicionar(Cliente cliente);
        Task<Cliente> ObterPorEmail(string email);
    }
}
