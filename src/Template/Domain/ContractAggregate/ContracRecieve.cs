using Core.Cqrs.Domain;
using Core.Cqrs.Domain.Domain;

namespace Template.Domain.ContractAggregate
{
    public class ContracRecieve : BaseEntity, IAggregateRoot
    {
        public Guid NutritionalPlanId { get; private set; }
        public Guid ClientId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public string PlanDetails { get; private set; }
        public Guid ContracGuid { get; private set; }

        private ContracRecieve()
        {
            PlanDetails = string.Empty;
        }

        internal ContracRecieve(Guid nutritionalPlanId, Guid clientId, Guid contracGuid, DateTime createdAt, string planDetails) : this()
        {
            NutritionalPlanId = nutritionalPlanId;
            ClientId = clientId;
            ContracGuid = contracGuid;
            CreatedAt = createdAt;
            PlanDetails = planDetails;
        }
    }
}