using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Models;
using Ordering.Infrastructure.Mail;
using Ordering.Infrastructure.Persistence;
using Ordering.Infrastructure.Repositories;

namespace Ordering.Api.Configs
{
    public static class ApiConfig
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<OrderContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("OrderingConnectionString"), SqlOptions =>
                {
                    SqlOptions.MigrationsHistoryTable("ef_migrations_history");
                    SqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(150), null);
                    SqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                }));

            services.AddEntityFrameworkNpgsql().AddDbContext<OrderContext>(options => {

                options.EnableSensitiveDataLogging();


            });



            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
            services.AddScoped<IOrderRepository, OrderRepository>();

            //services.Configure<EmailSettings>(c => configuration.GetSection("EmailSettings"));


            var settings = new EmailSettings();
            new ConfigureFromConfigurationOptions<EmailSettings>(configuration.GetSection("EmailSettings")).Configure(settings);
            services.AddSingleton(settings);



            services.AddTransient<IEmailService, EmailService>();
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            return services;
        }
    }
}
