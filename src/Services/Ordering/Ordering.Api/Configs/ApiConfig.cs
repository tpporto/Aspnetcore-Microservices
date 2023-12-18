using MassTransit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Ordering.Application;
using Ordering.Infrastructure;

namespace Ordering.Api.Configs
{
    public static class ApiConfig
    {
        public static IServiceCollection WebApiConfigService(this IServiceCollection services, IConfiguration configuration)
        {
            // Add services to the container.
            services.AddApplicationServices();
            services.AddInfrastructureServices(configuration);
            /*

            // MassTransit-RabbitMQ Configuration
            services.AddMassTransit(config => {

                config.AddConsumer<BasketCheckoutConsumer>();

                config.UsingRabbitMq((ctx, cfg) => {
                    cfg.Host(configuration["EventBusSettings:HostAddress"]);

                    cfg.ReceiveEndpoint(EventBusConstants.BasketCheckoutQueue, c =>
                    {
                        c.ConfigureConsumer<BasketCheckoutConsumer>(ctx);
                    });
                });
            });
            services.AddMassTransitHostedService();
            */
            // General Configuration
            services.AddAutoMapper(Assembly.GetEntryAssembly());

            //services.AddScoped<BasketCheckoutConsumer>();


            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Ordering.API", Version = "v1" });
            });


            return services;
        }
        public static IApplicationBuilder WebApiConfigApplication(this WebApplication app, IWebHostEnvironment env)
        {

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            // Configure the HTTP request pipeline.

            app.UseAuthorization();

            app.MapControllers();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ordering.API v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            return app;
        }

    }
}
