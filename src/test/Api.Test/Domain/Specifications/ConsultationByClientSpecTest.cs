using Ardalis.Specification;
using Template.Domain.MedicalConsultationAggregate;
using Template.Domain.MedicalConsultationAggregate.Specification;

namespace Api.Test.Domain.Specifications
{
    public class ConsultationByClientSpecTest
    {
        [Test]
        public void ConsultationByClientSpec_ShouldCreateCorrectSpecification()
        {
            // Arrange
            var clientId = Guid.NewGuid();

            // Act
            var spec = new ConsultationByClientSpec(clientId);

            // Assert
            Assert.That(spec, Is.Not.Null);
            Assert.That(spec, Is.InstanceOf<Specification<Consultation>>());
        }

        [Test]
        public void ConsultationByClientSpec_WithValidClientId_ShouldReturnSpecification()
        {
            // Arrange
            var clientId = Guid.NewGuid();
            var spec = new ConsultationByClientSpec(clientId);

            // Act & Assert
            Assert.That(spec, Is.Not.Null);
        }
    }
}