using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.ApplicationInsights.Extensibility;
using Template.Api.BackgroundsServices;
using Template.Api.Extensions;
using Template.Command;
using Template.Command.Database;
using Template.ServiceDefaults;
using Template.Services;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationInsightsTelemetry();
builder.AddServiceDefaults();
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Services.ConfigureResponseCaching();
// Add services        builder.Services.AddApplicationInsightsTelemetry();
builder.Services.AddControllers();
builder.AddNpgsqlDbContext<DataBaseContext>("nutri_solid_database");

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddApplication(builder.Configuration);

//Background service
builder.Services.AddHostedService<AzureEventHubConsumerHostedService>();
builder.Services.AddHostedService<OutboxPublisherService>();


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "_myAllowSpecificOrigins",
                      policy =>
                      {
                          policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                      });
});

builder.ConfigureSwagger();

// The following line enables Application Insights telemetry collection.

builder.Services.AddSingleton<ITelemetryInitializer>(new RoleNameTelemetryInitializer("Coordinacion-BackEnd-Catering"));

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new DefaultInfrastructureModule(builder.Environment.EnvironmentName == "Development"));
});



var app = builder.Build();

app.MapDefaultEndpoints();

// O bien si quieres usarlo siempre (desarrollo y producci�n):
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("_myAllowSpecificOrigins");
app.MapControllers();

app.UseRouting();

app.Run();


public partial class Program { } // ?? Agrega esta l�nea