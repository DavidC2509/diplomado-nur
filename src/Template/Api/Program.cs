using Autofac;
using Autofac.Extensions.DependencyInjection;
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

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new DefaultInfrastructureModule(builder.Environment.EnvironmentName == "Development"));
});
builder.Services.AddApplicationInsightsTelemetry(new Microsoft.ApplicationInsights.AspNetCore.Extensions.ApplicationInsightsServiceOptions
{
    ConnectionString = builder.Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"]
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