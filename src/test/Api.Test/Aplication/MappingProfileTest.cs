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
            var client = Client.CreateClient("David 2", "75324397112", "davidfernando.chavez777331@gmail.com");

            // Act
            var clientModel = _mapper.Map<ClientModel>(client);

            // Assert
            Assert.IsNotNull(clientModel);
            Assert.AreEqual(client.Name, clientModel.Name);
            Assert.AreEqual(client.Phone, clientModel.Phone);
        }

        [Test]
        public void Should_Map_Address_To_AddresModel()
        {
            // Arrange
            var address = Address.StoreAddres("Street 1", "City 1", 10.0m, 20.0m);

            // Act
            var addressModel = _mapper.Map<AddresModel>(address);

            // Assert
            Assert.IsNotNull(addressModel);
            Assert.AreEqual(address.Street, addressModel.Street);
            Assert.AreEqual(address.City, addressModel.City);
            Assert.AreEqual(address.Latituded, addressModel.Latituded);
            Assert.AreEqual(address.Longitud, addressModel.Longitud);
        }
    }
}


