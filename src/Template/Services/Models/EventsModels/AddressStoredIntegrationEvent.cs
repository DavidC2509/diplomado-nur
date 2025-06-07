namespace Template.Services.Models.EventsModels
{
    public class AddressStoredIntegrationEvent
    {
        public Guid ClientGuid { get; set; }
        public string Street { get; set; } = default!;
        public string City { get; set; } = default!;
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public Guid AddressId { get; set; }

    }
}