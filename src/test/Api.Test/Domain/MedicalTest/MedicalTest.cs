using Template.Domain.MedicalConsultationAggregate;

namespace Api.Test.Domain.MedicalTest
{
    internal class MedicalTest
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
    }
}
