using Template.Domain.MedicalConsultationAggregate;

namespace Api.Test.Domain.MedicalTest
{
    public class MedicalTest
    {
        [Test]
        public void MedicalCreationIsValid()
        {
            // Arrange
            string description = "Consulta jueves";
            Guid consultExternal = Guid.NewGuid();
            Guid clientId = Guid.NewGuid();

            // Act
            var consult = Consultation.CreateConsult(description, consultExternal, clientId, true);
            Assert.Multiple(() =>
            {
                // Assert
                Assert.That(consult.Descripcion, Is.EqualTo(description));
                Assert.That(consult.IdClient, Is.EqualTo(clientId));
                Assert.That(consult.Status, Is.EqualTo(true));
                Assert.That(consult.IdConsultExternal, Is.EqualTo(consultExternal));
            });
        }

        [Test]
        public void MedicalCreationWithInactiveStatusIsValid()
        {
            // Arrange
            string description = "Consulta cancelada";
            Guid consultExternal = Guid.NewGuid();
            Guid clientId = Guid.NewGuid();

            // Act
            var consult = Consultation.CreateConsult(description, consultExternal, clientId, false);
            Assert.Multiple(() =>
            {
                // Assert
                Assert.That(consult.Descripcion, Is.EqualTo(description));
                Assert.That(consult.IdClient, Is.EqualTo(clientId));
                Assert.That(consult.Status, Is.EqualTo(false));
                Assert.That(consult.IdConsultExternal, Is.EqualTo(consultExternal));
            });
        }

        [Test]
        public void MedicalCreationWithEmptyDescriptionIsValid()
        {
            // Arrange
            string description = "";
            Guid consultExternal = Guid.NewGuid();
            Guid clientId = Guid.NewGuid();

            // Act
            var consult = Consultation.CreateConsult(description, consultExternal, clientId, true);
            Assert.Multiple(() =>
            {
                // Assert
                Assert.That(consult.Descripcion, Is.EqualTo(description));
                Assert.That(consult.IdClient, Is.EqualTo(clientId));
                Assert.That(consult.Status, Is.EqualTo(true));
                Assert.That(consult.IdConsultExternal, Is.EqualTo(consultExternal));
            });
        }

        [Test]
        public void MedicalCreationWithLongDescriptionIsValid()
        {
            // Arrange
            string description = "Esta es una descripción muy larga para una consulta médica que incluye muchos detalles sobre el estado del paciente y los síntomas que presenta";
            Guid consultExternal = Guid.NewGuid();
            Guid clientId = Guid.NewGuid();

            // Act
            var consult = Consultation.CreateConsult(description, consultExternal, clientId, true);
            Assert.Multiple(() =>
            {
                // Assert
                Assert.That(consult.Descripcion, Is.EqualTo(description));
                Assert.That(consult.IdClient, Is.EqualTo(clientId));
                Assert.That(consult.Status, Is.EqualTo(true));
                Assert.That(consult.IdConsultExternal, Is.EqualTo(consultExternal));
            });
        }

        [Test]
        public void MedicalConsultationIdIsGenerated()
        {
            // Arrange
            string description = "Consulta test";
            Guid consultExternal = Guid.NewGuid();
            Guid clientId = Guid.NewGuid();

            // Act
            var consult = Consultation.CreateConsult(description, consultExternal, clientId, true);

            // Assert
            Assert.That(consult.Descripcion, Is.EqualTo(description));
        }
    }
}