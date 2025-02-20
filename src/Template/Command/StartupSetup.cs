using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Template.Command.Database;

namespace Template.Command
{
    public static class StartupSetup
    {
        public static void AddDbContext(this IServiceCollection services, string connectionString)
        {

            services.AddDbContext<DataBaseContext>(options =>
              options.UseNpgsql(
                connectionString,
                psqlOptions =>
                {
                    psqlOptions.MigrationsAssembly("Template.Command").EnableRetryOnFailure();
                    psqlOptions.ExecutionStrategy(c => new RetryingPostgresExecutionStrategy(c));
                }));
        }


    }
}
