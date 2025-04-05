using Core.Cqrs.CommandAndQueryHandler;
using Core.Cqrs.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Reflection;
using Template.Domain.Events;

namespace Template.Command.Database
{
    public class DataBaseContext : BaseDbContext, IUnitOfWork
    {

        public DataBaseContext(DbContextOptions<DataBaseContext> options, IMediator mediator)
            : base(options, mediator)
        {
        }

        public override string Owner => "template";
        public override string TablePrefix => "template";

        /// <summary>
        /// Todo los registros de las entidades se hacen aqui con sus configuraciones
        /// </summary>
        /// <param name="modelBuilder">Constructor de modelos</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        protected override void OnPreModelCreating(ModelBuilder modelBuilder)
        {

        }


        public new async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
            // performed through the DbContext will be committed
            var result = await this.SaveChangesAsync(cancellationToken);

            return true;
        }


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            OnBeforeSaving();
            await EventDispacher();
            int result = await base.SaveChangesAsync(cancellationToken);

            // dispatch events only if save was successful

            return result;
        }


        public async Task EventDispacher()
        {
            var entitiesWithEvents = ChangeTracker.Entries<IDataTenantId>()
              .Select(e => e.Entity)
              .Where(e => e.DomainEvents.Any())
                .ToArray();
            _mediator.DispatchAndClearEventsDomain(entitiesWithEvents);

            var entitiesWithEventsAwait = ChangeTracker.Entries<IDataTenantId>()
            .Select(e => e.Entity)
             .Where(e => e.DomainEventsAwait.Any())
               .ToArray();

            await _mediator.DispatchAndClearEventsDomaiAwait(entitiesWithEventsAwait);
        }

    }
}