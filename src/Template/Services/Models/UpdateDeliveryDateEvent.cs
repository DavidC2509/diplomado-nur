namespace Template.Services.Models
{
    public class UpdateDeliveryDateEvent
    {
        public Guid IdClient { get; set; }
        public DateTime PreviusDate { get; set; }
        public DateTime NewDate { get; set; }
        public Guid AddresGuid { get; set; }
    }
}
