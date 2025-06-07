namespace Template.Services.Models.EventsModels
{
    public class UpdateDeliveryDateEvent
    {
        public Guid ClientGuid { get; set; }
        public DateTime PreviousDate { get; set; }
        public DateTime NewDate { get; set; }
        public Guid AddressGuid { get; set; }
    }
}