using AutoMapper;
using Core.Cqrs.Domain.Repository;
using Moq;
using Template.Domain.ClientAggregate;
using Template.Domain.MedicalConsultationAggregate;
using Template.Domain.MedicalConsultationAggregate.Specification;
using Template.Services.Models;
using Template.Services.Query.MedicalConsultationQuery;

namespace Api.Test.Aplication.MedicalHandler
{
    class MedicalHandlerQueryTest
    {

        private readonly Mock<IReadRepository<Consultation>> _repository;
        private readonly IMapper _mapper;

        public MedicalHandlerQueryTest()
        {
            _repository = new Mock<IReadRepository<Consultation>>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MedicalIllnesses, MedicaIllnessesModel>();
                cfg.CreateMap<Consultation, MedicalConsultModel>();
            });
            _mapper = config.CreateMapper();
        }

        [Test]
        public async Task GetConsultQuery()
        {
            // Arrange
            var tcs = new CancellationTokenSource(1000);
            string description = "Consulta jueves";
            Guid consultExternal = Guid.NewGuid();
            Guid clientId = Guid.NewGuid();

            // Act
            var consult = Consultation.CreateConsult(description, consultExternal, clientId, true);

            _repository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<ConsultationByIdSpec>(), tcs.Token))
                         .ReturnsAsync(consult);

            var handler = new GetMedicalConsultationByIdQueryHandler(_repository.Object, _mapper);
            var query = new GetMedicalConsultationByIdQuery(consult.Id);

            // Act
            var result = await handler.Handle(query, tcs.Token);

            // Assert
            Assert.Multiple(() =>
            {
                _repository.Verify(x => x.FirstOrDefaultAsync(It.IsAny<ConsultationByIdSpec>(), tcs.Token), Times.Once);
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Descripcion, Is.EqualTo(consult.Descripcion));
                Assert.That(result.IdConsultExternal, Is.EqualTo(consult.IdConsultExternal));
            });
        }

        [Test]
        public async Task GetListConsultQuery()
        {
            // Arrange
            var tcs = new CancellationTokenSource(1000);
            string description = "Consulta jueves";
            Guid consultExternal = Guid.NewGuid();
            Guid clientId = Guid.NewGuid();

            // Act
            var consult = Consultation.CreateConsult(description, consultExternal, clientId, true);

            List<Consultation> consults = [consult];

            _repository.Setup(x => x.ListAsync(It.IsAny<ConsultationByClientSpec>(), tcs.Token))
                         .ReturnsAsync(consults);

            var handler = new ListMedicalConsultationByClientQueryHandler(_repository.Object, _mapper);
            var query = new ListMedicalConsultationByClientQuery(Guid.NewGuid());

            // Act
            var result = await handler.Handle(query, tcs.Token);

            // Assert
            Assert.Multiple(() =>
            {
                _repository.Verify(x => x.ListAsync(It.IsAny<ConsultationByClientSpec>(), tcs.Token), Times.Once);
                Assert.That(result, Is.Not.Null);
            });
        }

    }
}
