using Core.Cqrs.Domain.Repository;
using Moq;
using Template.Domain.MedicalConsultationAggregate;
using Template.Services.Command.MedicalConsultCommand;

namespace Api.Test.Aplication.MedicalHandler
{
    class MedicalHandlerTest
    {

        private readonly Mock<IRepository<Consultation>> _repository;
        public MedicalHandlerTest()
        {
            _repository = new Mock<IRepository<Consultation>>();
        }

        [Test]
        public async Task ConsultationCommandTestHandler()
        {
            // Arrange
            var tcs = new CancellationTokenSource(1000);
            string name = "Consulta 2";
            Guid idConsultExternal = Guid.NewGuid();
            Guid idClient = Guid.NewGuid();

            var consultation = Consultation.CreateConsult(name, idConsultExternal, idClient, true);


            _repository.Setup(x => x.Add(consultation))
                         .Returns(consultation);

            _repository.Setup(x => x.UnitOfWork.SaveEntitiesAsync(tcs.Token));

            // Act
            StoreMedicalConsultCommandHandler command
            = new StoreMedicalConsultCommandHandler(_repository.Object);

            StoreMedicalConsultCommand storeClientCommand = new StoreMedicalConsultCommand
            {
                Name = name,
                IdClient = idClient,
                IdConsultExternal = idConsultExternal,
                Status = true
            };
            await command.Handle(storeClientCommand, tcs.Token);

            // Assert
            _repository.Verify(x => x.Add(It.IsAny<Consultation>()), Times.Once);
            _repository.Verify(x => x.UnitOfWork.SaveEntitiesAsync(tcs.Token), Times.Once);

        }
    }
}
