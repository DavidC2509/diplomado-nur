namespace Template.Domain.Interfaz
{
    public interface IEventDispatcher
    {
        public Task DispatchAsync(IEnumerable<IDomainEvent> events);
        public Task DispatchAsync(IDomainEvent ev);

    }
}
