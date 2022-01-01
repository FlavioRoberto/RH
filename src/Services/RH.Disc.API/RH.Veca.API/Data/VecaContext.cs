using Microsoft.EntityFrameworkCore;
using RH.Core.Data;
using RH.Veca.API.Domain.Models;
using System.Threading.Tasks;

namespace RH.Veca.API.Data
{
    public class VecaContext : DbContext, IUnitOfWork
    {
        public VecaContext(DbContextOptions<VecaContext> options) : base(options)
        {
        }

        public DbSet<Questionario> Questionario { get; set; }

        public async Task<bool> Commit()
        {
            return await base.SaveChangesAsync() > 0;
        }
    }
}
