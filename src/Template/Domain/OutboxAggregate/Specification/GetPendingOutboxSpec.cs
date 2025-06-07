using Ardalis.Specification;

namespace Template.Domain.OutboxAggregate.Specification
{
    public class GetPendingOutboxSpec : Specification<OutboxMessage>
    {
        public GetPendingOutboxSpec()
        {
            Query
                 .Where(x => !x.Sent)
                    .OrderBy(x => x.Timestamp);
        }
    }
}