using Core.Cqrs.CommandAndQueryHandler.Repository;
using Core.Cqrs.Domain;
using Core.Cqrs.Domain.Domain;
using Core.Cqrs.Domain.Repository;
namespace Template.Command.Database
{
    public class EfRepository<T> : BaseRepository<T, DataBaseContext>, IRepository<T> where T : BaseEntity, IAggregateRoot
    {
        public EfRepository(DataBaseContext context) : base(context) { }
    }
}