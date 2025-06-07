using AutoMapper;
using Template.Domain.ClientAggregate;
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
    }
}