namespace Template.Services.Models.EventsModels
{
    public class UpdateDeliveryBlockDateEvent
    {
        public Guid ClientGuid { get; set; }
        public DateTime ToDate { get; set; }
        public DateTime FromDate { get; set; }
        public Guid AddressGuid { get; set; }
    }
}