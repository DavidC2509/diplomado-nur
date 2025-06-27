using Template.Domain.ValueObjects;

namespace Api.Test.Domain.ValueObjects
{
    public class EmailValueObjectTest
    {
        [Test]
        public void EmailValueObject_WithValidEmail_ShouldCreateSuccessfully()
        {
            // Arrange
            var validEmail = "test@example.com";

            // Act
            var emailValueObject = new EmailValueObject(validEmail);

            // Assert
            Assert.That(emailValueObject.Value, Is.EqualTo(validEmail));
        }

        [Test]
        public void EmailValueObject_WithInvalidEmail_ShouldThrowArgumentException()
        {
            // Arrange
            var invalidEmail = "invalid-email";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => EmailValueObject.Create(invalidEmail));
        }

        [Test]
        public void EmailValueObject_WithEmptyEmail_ShouldThrowArgumentException()
        {
            // Arrange
            var emptyEmail = "";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => EmailValueObject.Create(emptyEmail));
        }

        [Test]
        public void EmailValueObject_WithNullEmail_ShouldThrowArgumentException()
        {
            // Arrange
            string nullEmail = "null";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => EmailValueObject.Create(nullEmail));
        }

        [Test]
        public void EmailValueObject_WithWhitespaceEmail_ShouldThrowArgumentException()
        {
            // Arrange
            var whitespaceEmail = "   ";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => EmailValueObject.Create(whitespaceEmail));
        }

        [Test]
        public void EmailValueObject_WithComplexValidEmail_ShouldCreateSuccessfully()
        {
            // Arrange
            var complexEmail = "user.name+tag@domain.co.uk";

            // Act
            var emailValueObject = new EmailValueObject(complexEmail);

            // Assert
            Assert.That(emailValueObject.Value, Is.EqualTo(complexEmail));
        }

        [Test]
        public void EmailValueObject_ToString_ShouldReturnEmailValue()
        {
            // Arrange
            var email = "test@example.com";
            var emailValueObject = EmailValueObject.Create(email);
            // Act
            var result = emailValueObject.Value;

            // Assert
            Assert.That(result, Is.EqualTo(email));
        }
    }
}