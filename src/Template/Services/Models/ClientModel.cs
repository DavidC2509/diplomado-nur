namespace Template.Services.Models
{
    public class ClientModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Phone { get; set; }
        public string Email { get; set; }
        public List<AddresModel> Addresses { get; set; }
        //public List<MedicaIllnessesModel> MedicalIllnessess { get; set; }

    }
}
