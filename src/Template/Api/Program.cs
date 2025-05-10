using Autofac;
using Autofac.Extensions.DependencyInjection;
using Azure.Monitor.OpenTelemetry.AspNetCore;
using Template.Api.Extensions;
using Template.Command;
using Template.Command.Database;
using Template.ServiceDefaults;
using Template.Services;


var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Services.ConfigureResponseCaching();

// Add services to the container.
builder.Services.AddControllers();

builder.AddNpgsqlDbContext<DataBaseContext>("nutri_solid_database");

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddApplication();

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

builder.Services.AddApplicationInsightsTelemetry();
// Add OpenTelemetry and configure it to use Azure Monitor.
builder.Services.AddOpenTelemetry().UseAzureMonitor();

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