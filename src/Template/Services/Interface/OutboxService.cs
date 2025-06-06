using Core.Cqrs.Domain.Repository;
using System.Text.Json;
using Template.Domain.Interfaz;
using Template.Domain.OutboxAggregate;
using Template.Domain.OutboxAggregate.Specification;

namespace Template.Services.Interface
{
    public class OutboxService : IOutboxService
    {
        private readonly IRepository<OutboxMessage> _repository;

        public OutboxService(IRepository<OutboxMessage> repository)
        {
            _repository = repository;
        }

        public async Task<List<OutboxMessage>> GetPendingAsync(CancellationToken cancellationToken)
        {
            var specification = new GetPendingOutboxSpec();

            return await _repository.ListAsync(specification, cancellationToken);
        }

        public async Task MarkAsSentAsync(Guid messageId, CancellationToken cancellationToken)
        {
            var outbox = await _repository.GetByIdAsync(messageId, cancellationToken);

            if (outbox != null)
            {
                outbox.UpdateSendOutbox();
                _repository.Update(outbox);
                await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            }
        }

        public async Task SaveAsync<T>(T data, string eventType, CancellationToken cancellationToken)
        {
            var json = JsonSerializer.SerializeToElement(data);
            var outbox = OutboxMessage.StoreOutbox(eventType, json);
            _repository.Add(outbox);
            await _repository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}