using Template.Domain.RequestChangeAggregate;

namespace Api.Test.Domain.RequestChangeTest
{
    internal class RequestChangeTest
    {
        [Test]
        public void RequestChangeCreationIsValid()
        {
            // Arrange
            DateTime previusDate = DateTime.Now;
            DateTime newDate = DateTime.Now;
            Guid idAppointment = Guid.NewGuid();
            Guid idClient = Guid.NewGuid();

            // Act
            var requestChange = RequestChangeHistory.CreateChangeHistory(idAppointment, idClient, previusDate, newDate);
            Assert.Multiple(() =>
            {
                // Assert
                Assert.That(requestChange.IdClient, Is.EqualTo(idClient));
                Assert.That(requestChange.AppointmentGuid, Is.EqualTo(idAppointment));
                Assert.That(requestChange.NewDate, Is.EqualTo(newDate));
                Assert.That(requestChange.PreviusDate, Is.EqualTo(previusDate));

            });

        }
    }
}