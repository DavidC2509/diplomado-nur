namespace Template.Services.EventsRecive.Models
{
    public class ContracRecieveHandler
    {
        public Guid NutritionalPlanId { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid ClientId { get; set; }
        public string PlanDetails { get; set; } = string.Empty;
        public Guid Id { get; set; }
    }
}