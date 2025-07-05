using Core.Cqrs.Domain;
using Core.Cqrs.Domain.Domain;
using Template.Domain.ValueObjects;

namespace Template.Domain.ClientAggregate
{
    public class Client : BaseEntity, IAggregateRoot
    {
        public string Name { get; private set; }
        public string? Phone { get; private set; }
        public EmailValueObject Email { get; private set; }
        private readonly List<Address> _addresses;
        public IEnumerable<Address> Addresses => _addresses.AsReadOnly();

        private readonly List<MedicalIllnesses> _medicalIllnesses;
        public IEnumerable<MedicalIllnesses> MedicalIllnessess => _medicalIllnesses.AsReadOnly();

        private Client()
        {
            Name = string.Empty;
            _addresses = [];
            _medicalIllnesses = [];
            Email = new EmailValueObject();
        }

        internal Client(string name, string phone, string email, Guid id) : this()
        {
            Name = name;
            Phone = phone;
            EmailValueObject emailObject = EmailValueObject.Create(email);
            Email = emailObject;
            Id = id;
        }

        public static Client CreateClient(string name, string phone, string email, Guid clientGuid)
            => new(name, phone, email, clientGuid);

        public void AddAddres(string street, string city, decimal latituded, decimal longitud, DateTime deliveryDate)
        {
            _addresses.ForEach(address => address.UpdateStatus(false));
            _addresses.Add(Address.StoreAddres(street, city, latituded, longitud, deliveryDate));
        }

        public void UpdateDateAddres(Guid addresGuid, DateTime deliveryDate, Guid clientGuid)
        {
            _addresses.Find(address => address.Id == addresGuid)?.UpdateDateDelivery(deliveryDate, clientGuid);
        }

        public void AddMedicalIllnesses(string name, string descripcion, string type)
        {
            _medicalIllnesses.Add(MedicalIllnesses.StoreMedicalIllnesses(name, descripcion, type));
        }

        public void NotificacionAddres(Guid clientGuid)
        {
            _addresses?.LastOrDefault()?.NotificationEvent(clientGuid);
        }



    }
}