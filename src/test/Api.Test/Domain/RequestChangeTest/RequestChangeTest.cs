using Template.Domain.RequestChangeAggregate;

namespace Api.Test.Domain.RequestChangeTest
{
    public class RequestChangeTest
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

        [Test]
        public void RequestChangeCreationWithFutureDateIsValid()
        {
            // Arrange
            DateTime previusDate = DateTime.Now;
            DateTime newDate = DateTime.Now.AddDays(7);
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

        [Test]
        public void RequestChangeCreationWithPastDateIsValid()
        {
            // Arrange
            DateTime previusDate = DateTime.Now;
            DateTime newDate = DateTime.Now.AddDays(-1);
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

        [Test]
        public void RequestChangeCreationWithSameDateIsValid()
        {
            // Arrange
            DateTime sameDate = DateTime.Now;
            Guid idAppointment = Guid.NewGuid();
            Guid idClient = Guid.NewGuid();

            // Act
            var requestChange = RequestChangeHistory.CreateChangeHistory(idAppointment, idClient, sameDate, sameDate);
            Assert.Multiple(() =>
            {
                // Assert
                Assert.That(requestChange.IdClient, Is.EqualTo(idClient));
                Assert.That(requestChange.AppointmentGuid, Is.EqualTo(idAppointment));
                Assert.That(requestChange.NewDate, Is.EqualTo(sameDate));
                Assert.That(requestChange.PreviusDate, Is.EqualTo(sameDate));
            });
        }

        [Test]
        public void RequestChangeHistoryIdIsGenerated()
        {
            // Arrange
            DateTime previusDate = DateTime.Now;
            DateTime newDate = DateTime.Now.AddDays(1);
            Guid idAppointment = Guid.NewGuid();
            Guid idClient = Guid.NewGuid();

            // Act
            var requestChange = RequestChangeHistory.CreateChangeHistory(idAppointment, idClient, previusDate, newDate);

            // Assert
            Assert.That(requestChange.IdClient, Is.EqualTo(idClient));
        }
    }
}