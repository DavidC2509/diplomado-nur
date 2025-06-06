using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using Microsoft.Extensions.Options;
using System.Text;
using Template.Domain.Interfaz.EventBus;

namespace Template.Services.ServciesBus
{
    public class AzureServiceBusService : IEventBusService
    {
        private readonly string _connectionString;

        public AzureServiceBusService(IOptions<ServiceBusSettings> settings)
        {
            _connectionString = settings.Value.ConnectionString;
        }

        public async Task SendMessageAsync(string eventHubName, string message)
        {
            try
            {
                EventHubProducerClient client = new EventHubProducerClient(
              _connectionString,
             eventHubName);

                using var batch = await client.CreateBatchAsync();

                if (!batch.TryAdd(new EventData(Encoding.UTF8.GetBytes(message))))
                    throw new InvalidOperationException("El mensaje es demasiado grande para el batch.");

                await client.SendAsync(batch);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }

}