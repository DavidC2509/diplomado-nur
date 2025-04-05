using Core.Cqrs.Domain;
using Core.Cqrs.Domain.Domain;

namespace Template.Domain.ClientAggregate
{
    public class AddressHistory : BaseEntity, IAggregateChild<Address>
    {
        public Guid IdDelivery { get; private set; }
    }
}