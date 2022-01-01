using System.Threading.Tasks;

namespace RH.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
