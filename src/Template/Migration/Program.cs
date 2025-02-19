using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Template.Command;
using Template.Command.Database;
using Template.Migration;
using Template.ServiceDefaults;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddHostedService<ApiDbInitializer>();

builder.Services.AddOpenTelemetry()
        .WithTracing(tracing => tracing.AddSource(ApiDbInitializer.ActivitySourceName));

builder.ConfigureContainer(new AutofacServiceProviderFactory(), containerBuilder =>
{
    containerBuilder.RegisterModule(new DefaultInfrastructureModule(builder.Environment.EnvironmentName == "Development"));
});

builder.Services.AddDbContext(builder.Configuration.GetConnectionString("nutri-solid-database")!);

builder.EnrichNpgsqlDbContext<DataBaseContext>(settings =>
// Disable Aspire default retries as we're using a custom execution strategy
{
    settings.DisableRetry = true;
    settings.DisableTracing = true;
});

var host = builder.Build();

host.Run();
