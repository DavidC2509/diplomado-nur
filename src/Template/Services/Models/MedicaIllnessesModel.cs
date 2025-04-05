namespace Template.Services.Models
{
    public class MedicaIllnessesModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Descripcion { get; set; }
        public string Type { get; set; }
        public DateTime DateFound { get; set; }
    }
}