using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace Ordering.Api.Extensions
{
    public static class HostExtensions
    {
        //public static void MigrateDatabase<TContext>(this WebApplication app, int? retry = 0)
        public async static Task<WebApplication> MigrateDatabase<TContext>(this WebApplication app,
                                             Action<TContext, IServiceProvider> seeder,
                                             int? retry = 0) where TContext : DbContext
        {
            int retryForAvailability = retry.GetValueOrDefault();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var configuration = services.GetRequiredService<IConfiguration>();
                var logger = services.GetRequiredService<ILogger<TContext>>();
                var context = services.GetRequiredService<TContext>();

                try
                {
                    logger.LogInformation("Migrating postresql database.");

                    logger.LogInformation("Migrating database associated with context {DbContextName}", typeof(TContext).Name);

                    await InvokeSeeder(seeder, context, services);

                    logger.LogInformation("Migrated database associated with context {DbContextName}", typeof(TContext).Name);
                }
                catch (NpgsqlException ex)
                {
                    logger.LogError(ex, "An error occurred while migrating the postresql database");

                    if (retryForAvailability < 50)
                    {
                        retryForAvailability++;
                        System.Threading.Thread.Sleep(2000);
                        MigrateDatabase<TContext>(app, seeder, retryForAvailability);
                    }
                }
            }

            return app;
        }

        private async static Task InvokeSeeder<TContext>(Action<TContext, IServiceProvider> seeder,
                                                   TContext context,
                                                   IServiceProvider services)
                                                   where TContext : DbContext
        {
            await context.Database.MigrateAsync();
            seeder(context, services);
        }
    }
}