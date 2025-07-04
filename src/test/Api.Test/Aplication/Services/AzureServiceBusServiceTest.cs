//using Moq;
//using Template.Domain.Interfaz.EventBus;
//using Template.Services.ServciesBus;

//namespace Api.Test.Aplication.Services
//{
//    internal class AzureServiceBusServiceTest
//    {
//        private readonly Mock<ServiceBusSettings> _serviceBusSettings;
//        private readonly AzureServiceBusService _service;

//        public AzureServiceBusServiceTest()
//        {
//            _serviceBusSettings = new Mock<ServiceBusSettings>();
//            _serviceBusSettings.Object.ConnectionString = "test-connection-string";
//            _serviceBusSettings.Object.TopicName = "test-topic";

//            _service = new AzureServiceBusService(_serviceBusSettings.Object);
//        }

//        [Test]
//        public void AzureServiceBusService_ShouldBeCreated()
//        {
//            // Arrange & Act
//            var service = new AzureServiceBusService(_serviceBusSettings.Object);

//            // Assert
//            Assert.That(service, Is.Not.Null);
//        }

//        [Test]
//        public void AzureServiceBusService_WithNullSettings_ShouldThrowArgumentNullException()
//        {
//            // Act & Assert
//            Assert.Throws<ArgumentNullException>(() => new AzureServiceBusService(null));
//        }

//        [Test]
//        public void AzureServiceBusService_WithEmptyConnectionString_ShouldBeCreated()
//        {
//            // Arrange
//            var settings = new ServiceBusSettings
//            {
//                ConnectionString = "",
//                TopicName = "test-topic"
//            };

//            // Act
//            var service = new AzureServiceBusService(settings);

//            // Assert
//            Assert.That(service, Is.Not.Null);
//        }

//        [Test]
//        public void AzureServiceBusService_WithEmptyTopicName_ShouldBeCreated()
//        {
//            // Arrange
//            var settings = new ServiceBusSettings
//            {
//                ConnectionString = "test-connection",
//                TopicName = ""
//            };

//            // Act
//            var service = new AzureServiceBusService(settings);

//            // Assert
//            Assert.That(service, Is.Not.Null);
//        }
//    }
//} 