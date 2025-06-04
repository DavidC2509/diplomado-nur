namespace Template.Domain.Interfaz
{
    public interface IServiceBusConsumer
    {
        Task ReceiveMessagesAsync(CancellationToken cancellationToken = default);
    }
}
