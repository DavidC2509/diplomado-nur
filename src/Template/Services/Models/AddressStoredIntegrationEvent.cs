namespace Template.Services.Models
{
    public class AddressStoredIntegrationEvent
    {
        public Guid ClientGuid { get; set; }
        public string Street { get; set; } = default!;
        public string City { get; set; } = default!;
        public decimal Latituded { get; set; }
        public decimal Longitud { get; set; }
        public Guid AddresId { get; set; }

    }
}