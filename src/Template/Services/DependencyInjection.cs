using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Template.Domain.Interfaz;
using Template.Domain.Interfaz.EventBus;
using Template.Services.EventsRecive.Handler;
using Template.Services.Interface;
using Template.Services.ListBackgroundService;
using Template.Services.ServciesBus;

namespace Template.Services
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            // Todos los repositorys
            // Configuración de Azure Service Bus
            services.Configure<ServiceBusSettings>(configuration.GetSection("ServiceBusSettings"));

            services.AddScoped<IEventBusService, AzureServiceBusService>(); // Solo para enviar
            services.AddHostedService<AzureEventHubConsumerHostedService>(); // Escucha mensajes y ejecuta handlers

            //Handler recieved
            services.AddScoped<UserCreateReciveIntegrationEventHandler>();

            //Outbox
            services.AddScoped<IOutboxService, OutboxService>();
            services.AddHostedService<OutboxPublisherService>();


            return services;
        }

    }
}