using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using RH.Clientes.API.Domain.Models;
using RH.Core.Data;
using RH.Core.DomainObjects;
using RH.Core.Mediator;
using RH.Core.Messages;
using System.Linq;
using System.Threading.Tasks;

namespace RH.Clientes.API.Infra.Data
{
    public class ClientesContext : DbContext, IUnitOfWork
    {
        private readonly IMediatorHandler _mediatorHandler;

        public ClientesContext(DbContextOptions<ClientesContext> options,
                               IMediatorHandler mediatorHandler) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
            _mediatorHandler = mediatorHandler;
        }

        public DbSet<Cliente> Cliente { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<ValidationResult>();
            modelBuilder.Ignore<Event>();
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClientesContext).Assembly);
        }

        public async Task<bool> Commit()
        {
            var sucesso = await base.SaveChangesAsync() > 0;

            if (sucesso)
                await _mediatorHandler.PublicarEventos(this);

            return sucesso;

        }
    }

    public static class MediatorExtension
    {
        public static async Task PublicarEventos<T>(this IMediatorHandler mediator, T ctx) where T : DbContext
        {
            var domainEntities = ctx.ChangeTracker
                                    .Entries<Entity>()
                                    .Where(x => x.Entity.Notificacoes != null && x.Entity.Notificacoes.Any());

            var domainEvents = domainEntities.SelectMany(x => x.Entity.Notificacoes)
                                             .ToList();

            domainEntities.ToList().ForEach(entity => entity.Entity.LimparEventos());

            var tasks = domainEvents.Select(async (domainEvent) =>
                  await mediator.PublicarEvento(domainEvent));

            await Task.WhenAll(tasks);
        }
    }
}
