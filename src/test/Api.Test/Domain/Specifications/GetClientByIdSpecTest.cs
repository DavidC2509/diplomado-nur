using Ardalis.Specification;
using Template.Domain.ClientAggregate;
using Template.Domain.ClientAggregate.Specification;

namespace Api.Test.Domain.Specifications
{
    public class GetClientByIdSpecTest
    {
        [Test]
        public void GetClientByIdSpec_ShouldCreateCorrectSpecification()
        {
            // Arrange
            var clientId = Guid.NewGuid();

            // Act
            var spec = new GetClientByIdSpec(clientId);

            // Assert
            Assert.That(spec, Is.Not.Null);
            Assert.That(spec, Is.InstanceOf<Specification<Client>>());
        }

        [Test]
        public void GetClientByIdSpec_WithValidId_ShouldReturnSpecification()
        {
            // Arrange
            var clientId = Guid.NewGuid();
            var spec = new GetClientByIdSpec(clientId);

            // Act & Assert
            Assert.That(spec, Is.Not.Null);
        }
    }
} 