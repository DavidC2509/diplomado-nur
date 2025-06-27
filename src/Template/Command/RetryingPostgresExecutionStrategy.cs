using Microsoft.EntityFrameworkCore.Storage;
using Npgsql;

namespace Template.Command
{
    public class RetryingPostgresExecutionStrategy : ExecutionStrategy
    {
        public RetryingPostgresExecutionStrategy(ExecutionStrategyDependencies dependencies)
            : base(dependencies, DefaultMaxRetryCount, DefaultMaxDelay)
        {
        }

        protected override bool ShouldRetryOn(Exception exception)
        {
            if (exception is NpgsqlException postgresEx)
            {
                // Ejemplo: reintentar si es un error de timeout o de conexión transitoria
                return postgresEx.IsTransient;
            }

            if (exception is TimeoutException)
            {
                return true;
            }

            return false;
        }
    }
}