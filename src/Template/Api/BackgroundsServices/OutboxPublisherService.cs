using System.Diagnostics;
using System.Text.Json;
using Template.Domain.Interfaz;
using Template.Domain.Interfaz.EventBus;

namespace Template.Api.BackgroundsServices
{
    public class OutboxPublisherService : BackgroundService
    {
        private readonly IEventBusService _eventBus;
        private readonly IOutboxService _outboxService;
        private readonly ILogger<OutboxPublisherService> _logger;

        public OutboxPublisherService(
            IEventBusService eventBus,
            IOutboxService outboxService,
            ILogger<OutboxPublisherService> logger)
        {
            _eventBus = eventBus;
            _outboxService = outboxService;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("OutboxPublisherService iniciado.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var pending = await _outboxService.GetPendingAsync(stoppingToken);
                    _logger.LogInformation("Se encontraron {Count} mensajes pendientes para enviar.", pending.Count());

                    foreach (var msg in pending)
                    {
                        try
                        {
                            // Si tiene traceparent, creamos un Activity a partir de él
                            Activity? activity = null;
                            if (!string.IsNullOrWhiteSpace(msg.TraceId))
                            {
                                var activityContext = ActivityContext.Parse(msg.TraceId, null);
                                activity = new Activity("OutboxPublish")
                                    .SetParentId(activityContext.TraceId.ToString())
                                    .SetIdFormat(ActivityIdFormat.W3C);
                                activity.Start();
                            }


                            var bodySend = JsonSerializer.Serialize(msg, new JsonSerializerOptions
                            {
                                WriteIndented = true,
                                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                            });

                            await _eventBus.SendMessageAsync("nutrinur", bodySend, msg.TraceId);

                            await _outboxService.MarkAsSentAsync(msg.Id, stoppingToken);
                            _logger.LogInformation("Mensaje con ID {MessageId} enviado y marcado como enviado.", msg.Id);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "Error al enviar el mensaje con ID {MessageId}", msg.Id);
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error al obtener o procesar los mensajes pendientes.");
                }

                await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
            }

            _logger.LogInformation("OutboxPublisherService detenido.");
        }
    }
}