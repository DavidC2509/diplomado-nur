using Core.Cqrs.Domain.Repository;
using Moq;
using Template.Domain.ClientAggregate;
using Template.Services.Command.ClientCommand;

namespace Api.Test.Aplication.ClientHandler
{
    internal class ClientHandlerStoreTest
    {
        private readonly Mock<IRepository<Client>> _repository;
        public ClientHandlerStoreTest()
        {
            _repository = new Mock<IRepository<Client>>();
        }

        [Test]
        public async Task ClientStoreCommandTest()
        {
            // Arrange
            var tcs = new CancellationTokenSource(1000);
            string name = "David 2";
            string phone = "75324397112";
            string email = "davidfernando.chavez777331@gmail.com";
            var client = Client.CreateClient(name, phone, email, Guid.NewGuid());

            _repository.Setup(x => x.Add(client))
                         .Returns(client);

            _repository.Setup(x => x.UnitOfWork.SaveEntitiesAsync(tcs.Token));

            // Act
            StoreClientCommandHandler command
            = new StoreClientCommandHandler(_repository.Object);

            StoreClientCommand storeClientCommand = new StoreClientCommand
            {
                Name = name,
                Phone = phone,
                Email = email
            };
            await command.Handle(storeClientCommand, tcs.Token);

            // Assert
            _repository.Verify(x => x.Add(It.IsAny<Client>()), Times.Once);
            _repository.Verify(x => x.UnitOfWork.SaveEntitiesAsync(tcs.Token), Times.Once);
        }

        [Test]
        public async Task AddMedicalCommandTest()
        {
            // Arrange
            var tcs = new CancellationTokenSource(1000);
            string name = "David331";
            string phone = "75324397123";
            string email = "davidfernando.chavez777321@gmail.com";
            var client = Client.CreateClient(name, phone, email, Guid.NewGuid());


            _repository.Setup(x => x.GetByIdAsync(client.Id, tcs.Token))
                         .ReturnsAsync(client);

            _repository.Setup(x => x.Update(client))
                         .Returns(client);

            _repository.Setup(x => x.UnitOfWork.SaveEntitiesAsync(tcs.Token));

            // Act
            AddMedicalIllnessesCommandHandler command
            = new AddMedicalIllnessesCommandHandler(_repository.Object);

            AddMedicalIllnessesCommand storeClientCommand = new AddMedicalIllnessesCommand
            {
                Descripcion = "Dolor de cabeza",
                IdClient = client.Id,
                Name = "Dolor de cabeza",
                Type = "Dolor"
            };

            await command.Handle(storeClientCommand, tcs.Token);

            // Assert
            _repository.Verify(x => x.GetByIdAsync(client.Id, tcs.Token), Times.Once);
            _repository.Verify(x => x.UnitOfWork.SaveEntitiesAsync(tcs.Token), Times.Once);

        }

        [Test]
        public async Task AddAddresCommandTest()
        {
            // Arrange
            var tcs = new CancellationTokenSource(1000);
            string name = "David";
            string phone = "75324397";
            string email = "davidfernando.chavez777@gmail.com";
            var client = Client.CreateClient(name, phone, email, Guid.NewGuid());


            _repository.Setup(x => x.GetByIdAsync(client.Id, tcs.Token))
                         .ReturnsAsync(client);

            _repository.Setup(x => x.Update(client))
                         .Returns(client);

            _repository.Setup(x => x.UnitOfWork.SaveEntitiesAsync(tcs.Token));

            // Act
            AddAddresByClientCommandHandler command
            = new AddAddresByClientCommandHandler(_repository.Object);

            AddAddresByClientCommand storeClientCommand = new AddAddresByClientCommand
            {
                City = "La Paz",
                IdClient = client.Id,
                Latituded = 0,
                Longitud = 0,
                Street = "Av. 6 de Agosto"
            };
            await command.Handle(storeClientCommand, tcs.Token);

            // Assert
            _repository.Verify(x => x.Update(It.IsAny<Client>()), Times.Once);
            _repository.Verify(x => x.UnitOfWork.SaveEntitiesAsync(tcs.Token), Times.Once);

        }
    }
}