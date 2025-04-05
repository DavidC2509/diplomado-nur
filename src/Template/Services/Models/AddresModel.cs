namespace Template.Services.Models
{
    public class AddresModel
    {
        public Guid Id { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public decimal Latituded { get; set; }
        public decimal Longitud { get; set; }
        public bool Status { get; set; }
    }
}