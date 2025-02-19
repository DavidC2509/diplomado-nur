using Core.Cqrs.Domain.Repository;
using Moq;
using Template.Domain.RequestChangeAggregate;
using Template.Services.Command.RequestChangeCommand;

namespace Api.Test.Aplication.RequestChangeHandler
{
    class RequestChangeHandlerTest
    {
        private readonly Mock<IRepository<RequestChangeHistory>> _repository;
        public RequestChangeHandlerTest()
        {
            _repository = new Mock<IRepository<RequestChangeHistory>>();
        }

        [Test]
        public async Task ConsultationCommandTestHandler()
        {
            // Arrange
            var tcs = new CancellationTokenSource(1000);
            Guid idAppointment = Guid.NewGuid();
            Guid idClient = Guid.NewGuid();
            DateTime previusDate = DateTime.Now;
            DateTime newDate = DateTime.Now;

            var requestChange = RequestChangeHistory.CreateChangeHistory(idAppointment, idClient, previusDate, newDate);



            _repository.Setup(x => x.Add(requestChange))
                         .Returns(requestChange);

            _repository.Setup(x => x.UnitOfWork.SaveEntitiesAsync(tcs.Token));

            // Act
            ModifiedRequestChangeCommandHandler command
            = new ModifiedRequestChangeCommandHandler(_repository.Object);

            ModifiedRequestChangeCommand storeClientCommand = new ModifiedRequestChangeCommand
            {
                IdClient = idClient,
                IdAppointment = idAppointment,
                NewDate = newDate,
                PreviusDate = previusDate
            };

            await command.Handle(storeClientCommand, tcs.Token);

            // Assert
            _repository.Verify(x => x.Add(It.IsAny<RequestChangeHistory>()), Times.Once);
            _repository.Verify(x => x.UnitOfWork.SaveEntitiesAsync(tcs.Token), Times.Once);

        }
    }
}
