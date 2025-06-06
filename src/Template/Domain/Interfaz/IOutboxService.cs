using Template.Domain.OutboxAggregate;

namespace Template.Domain.Interfaz
{
    public interface IOutboxService
    {
        Task SaveAsync<T>(T data, string eventType, CancellationToken cancellationToken);
        Task<List<OutboxMessage>> GetPendingAsync(CancellationToken cancellationToken);
        Task MarkAsSentAsync(Guid messageId, CancellationToken cancellationToken);
    }
}
