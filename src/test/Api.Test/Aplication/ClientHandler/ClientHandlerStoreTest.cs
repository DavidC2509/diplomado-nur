using Core.Cqrs.Domain;
using Core.Cqrs.Domain.Repository;
using Microsoft.AspNetCore.Components.Web;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Template.Domain.ClientAggregate;
using Template.Domain.RequestChangeAggregate;
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
            string name = "David";
            string phone = "75324397";
            string email = "davidfernando.chavez777@gmail.com";
            var client = Client.CreateClient(name, phone, email);

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
    }
}
