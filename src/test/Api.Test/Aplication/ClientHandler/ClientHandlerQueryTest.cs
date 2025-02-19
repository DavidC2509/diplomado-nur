using AutoMapper;
using Core.Cqrs.Domain.Repository;
using Moq;
using Template.Domain.ClientAggregate;
using Template.Domain.ClientAggregate.Specification;
using Template.Services.Models;
using Template.Services.Query.ClientQuery;

namespace Api.Test.Aplication.ClientHandler
{
    class ClientHandlerQueryTest
    {
        private readonly Mock<IReadRepository<Client>> _repository;
        private readonly IMapper _mapper;

        public ClientHandlerQueryTest()
        {
            _repository = new Mock<IReadRepository<Client>>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Client, ClientModel>();
                cfg.CreateMap<Address, AddresModel>();
            });
            _mapper = config.CreateMapper();
        }

        [Test]
        public async Task GetClientQuery()
        {
            // Arrange
            var tcs = new CancellationTokenSource(1000);
            string name = "David 2";
            string phone = "75324397112";
            string email = "davidfernando.chavez777331@gmail.com";
            var client = Client.CreateClient(name, phone, email);

            _repository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<GetClientByIdSpec>(), tcs.Token))
                         .ReturnsAsync(client);

            var handler = new GetClientQueryHandler(_repository.Object, _mapper);
            var query = new GetClientQuery(client.Id);

            // Act
            var result = await handler.Handle(query, tcs.Token);

            // Assert
            Assert.Multiple(() =>
            {
                _repository.Verify(x => x.FirstOrDefaultAsync(It.IsAny<GetClientByIdSpec>(), tcs.Token), Times.Once);
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Name, Is.EqualTo(client.Name));
                Assert.That(result.Phone, Is.EqualTo(client.Phone));
            });
        }

        [Test]
        public async Task GetListClientQuery()
        {
            // Arrange
            var tcs = new CancellationTokenSource(1000);
            string name = "David 2";
            string phone = "75324397112";
            string email = "davidfernando.chavez777331@gmail.com";
            var client = Client.CreateClient(name, phone, email);

            List<Client> clients = [client];

            _repository.Setup(x => x.ListAsync(tcs.Token))
                         .ReturnsAsync(clients);

            var handler = new ListClientQueryHandler(_repository.Object, _mapper);
            var query = new ListClientQuery();

            // Act
            var result = await handler.Handle(query, tcs.Token);

            // Assert
            Assert.Multiple(() =>
            {
                _repository.Verify(x => x.ListAsync(tcs.Token), Times.Once);
                Assert.That(result, Is.Not.Null);
            });
        }


    }
}


