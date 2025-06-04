using Template.Domain.Interfaz;
using Template.Domain.Interfaz.EventBus;

namespace Template.Services
{
    public sealed class EventDispatcher : IEventDispatcher
    {
        private readonly IEventBusService _eventBusService;

        public EventDispatcher(IEventBusService eventBusService)
        {
            _eventBusService = eventBusService;
        }

        public async Task DispatchAsync(IEnumerable<IDomainEvent> events)
        {
            foreach (IDomainEvent ev in events)
            {
                await DispatchAsync(ev);
            }
        }

        public async Task DispatchAsync(IDomainEvent ev)
        {
            switch (ev)
            {


                default:
                    throw new Exception($"Unknown event type: '{ev.GetType()}'");
            }
        }


    }
}
