using AutoMapper;
using Template.Domain.ClientAggregate;
using Template.Domain.MedicalConsultationAggregate;
using Template.Services.Mapper;
using Template.Services.Models;

namespace Api.Test.Aplication
{
    public class MappingProfileTest
    {
        private IMapper _mapper;

        [SetUp]
        public void SetUp()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = config.CreateMapper();
        }

        [Test]
        public void MappingProfile_ConfigurationIsValid()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            config.AssertConfigurationIsValid();
        }

        [Test]
        public void Should_Map_Client_To_ClientModel()
        {
            // Arrange
            var client = Client.CreateClient("David 2", "75324397112", "davidfernando.chavez777331@gmail.com", Guid.NewGuid());

            // Act
            var clientModel = _mapper.Map<ClientModel>(client);

            // Assert
            Assert.IsNotNull(clientModel);
            Assert.That(clientModel.Name, Is.EqualTo(client.Name));
            Assert.That(clientModel.Phone, Is.EqualTo(client.Phone));
        }

        [Test]
        public void Should_Map_Address_To_AddresModel()
        {
            // Arrange
            var address = Address.StoreAddres("Street 1", "City 1", 10.0m, 20.0m, DateTime.Now.ToUniversalTime());

            // Act
            var addressModel = _mapper.Map<AddresModel>(address);

            // Assert
            Assert.IsNotNull(addressModel);
            Assert.That(addressModel.Street, Is.EqualTo(address.Street));
            Assert.That(addressModel.City, Is.EqualTo(address.City));
            Assert.That(addressModel.Latituded, Is.EqualTo(address.Latituded));
            Assert.That(addressModel.Longitud, Is.EqualTo(address.Longitud));
        }

        [Test]
        public void Should_Map_Consultation_To_MedicalConsultModel()
        {
            // Arrange
            var consultation = Consultation.CreateConsult("Consulta Test", Guid.NewGuid(), Guid.NewGuid(), true);

            // Act
            var consultationModel = _mapper.Map<MedicalConsultModel>(consultation);

            // Assert
            Assert.IsNotNull(consultationModel);
            Assert.That(consultationModel.Descripcion, Is.EqualTo(consultation.Descripcion));
            Assert.That(consultationModel.IdClient, Is.EqualTo(consultation.IdClient));
            Assert.That(consultationModel.Status, Is.EqualTo(consultation.Status));
        }

        [Test]
        public void Should_Map_Client_With_Addresses_To_ClientModel()
        {
            // Arrange
            var client = Client.CreateClient("David 3", "75324397113", "david3@test.com", Guid.NewGuid());
            client.AddAddres("Street 2", "City 2", 15.0m, 25.0m, DateTime.Now.ToUniversalTime());

            // Act
            var clientModel = _mapper.Map<ClientModel>(client);

            // Assert
            Assert.IsNotNull(clientModel);
            Assert.That(clientModel.Name, Is.EqualTo(client.Name));
            Assert.That(clientModel.Phone, Is.EqualTo(client.Phone));
            Assert.That(clientModel.Addresses, Is.Not.Null);
            Assert.That(clientModel.Addresses.Count, Is.EqualTo(1));
        }

        [Test]
        public void Should_Map_Empty_Client_To_ClientModel()
        {
            // Arrange
            var client = Client.CreateClient("dav", "332131", "papapote3@gmail.com", Guid.NewGuid());

            // Act
            var clientModel = _mapper.Map<ClientModel>(client);

            // Assert
            Assert.IsNotNull(clientModel);
            Assert.That(clientModel.Name, Is.EqualTo(client.Name));
            Assert.That(clientModel.Phone, Is.EqualTo(client.Phone));
        }

        [Test]
        public void Should_Map_Consultation_With_InactiveStatus_To_MedicalConsultModel()
        {
            // Arrange
            var consultation = Consultation.CreateConsult("Consulta Inactiva", Guid.NewGuid(), Guid.NewGuid(), false);

            // Act
            var consultationModel = _mapper.Map<MedicalConsultModel>(consultation);

            // Assert
            Assert.IsNotNull(consultationModel);
            Assert.That(consultationModel.Descripcion, Is.EqualTo(consultation.Descripcion));
            Assert.That(consultationModel.Status, Is.EqualTo(consultation.Status));
        }
    }
}