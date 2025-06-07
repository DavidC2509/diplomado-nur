using System.Text.Json;
using Template.Domain.Interfaz;
using Template.Domain.Interfaz.EventBus;

namespace Template.Api.BackgroundsServices
{
    public class OutboxPublisherService : BackgroundService
    {
        private readonly IEventBusService _eventBus;
        private readonly IOutboxService _outboxService;

        public OutboxPublisherService(IEventBusService eventBus, IOutboxService outboxService)
        {
            _eventBus = eventBus;
            _outboxService = outboxService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var pending = await _outboxService.GetPendingAsync(stoppingToken);

                foreach (var msg in pending)
                {
                    try
                    {
                        string bodySend = JsonSerializer.Serialize(msg, new JsonSerializerOptions { WriteIndented = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

                        await _eventBus.SendMessageAsync("cateringhub", bodySend);

                        await _outboxService.MarkAsSentAsync(msg.Id, stoppingToken);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }

                await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken); // delay entre ciclos
            }
        }
    }
}
