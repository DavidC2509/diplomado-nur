//using Core.Cqrs.Domain;
//using Core.Cqrs.Domain.Repository;
//using Moq;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Template.Domain.RequestChangeAggregate;
//using Template.Services.HandlerEvents;
//using Xunit;

//namespace Api.Test.Aplication.RequestChangeEvent
//{
//    internal class RequestChangeEventTest
//    {
//        private readonly Mock<IRepository<RequestChangeHistory>> _repository;
//        private readonly Mock<IUnitOfWork> _unitOfWork;
//        public RequestChangeEventTest()
//        {
//            _repository = new Mock<IRepository<RequestChangeHistory>>();
//            _unitOfWork = new Mock<IUnitOfWork>();
//        }

//        [Fact]
//        public async Task HandleIsValidWithOneItem()
//        {
//            // Arrange
//            var tcs = new CancellationTokenSource(1000);

//            DateTime previusDate = DateTime.Now;
//            DateTime newDate = DateTime.Now;
//            Guid idAppointment = Guid.NewGuid();
//            Guid idClient = Guid.NewGuid();

//            var requestChange = RequestChangeHistory.CreateChangeHistory(idAppointment, idClient, previusDate, newDate);

//            _repository.Setup(x => x.GetByIdAsync(idClient, tcs.Token))
//                         .ReturnsAsync(requestChange);


//            NotifiedNutrionEventHandler eventHandler
//                = new NotifiedNutrionEventHandler();
//            Guid transactionId = Guid.NewGuid();
        
//            // Act
//            await eventHandler.Handle(transactionCompleted, tcs.Token);

//            // Assert
//            _repository.Verify(x => x.Add(It.IsAny<Item>()), Times.Once);
//            _unitOfWork.Verify(x => x.CommitAsync(tcs.Token), Times.Once);
//        }
//    }
//}
