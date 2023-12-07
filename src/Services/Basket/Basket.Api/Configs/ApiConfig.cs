using Basket.Api.GrpcServices;
using Basket.Api.Repositories;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Basket.Api.Configs
{
    public static class ApiConfig
    {
        public static IServiceCollection WebApiConfigService(this IServiceCollection services, IConfiguration configuration)
        {
            // Add services to the container.

            // Redis Configuration
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetValue<string>("CacheSettings:ConnectionString");
            });

            // General Configuration
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddAutoMapper(Assembly.GetEntryAssembly());

            // Grpc Configuration
            //services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>
            //    (o => o.Address = new Uri(configuration["GrpcSettings:DiscountUrl"]));
           
            services.AddScoped<DiscountGrpcService>();

            // MassTransit-RabbitMQ Configuration
            //services.AddMassTransit(config => {
            //    config.UsingRabbitMq((ctx, cfg) => {
            //        cfg.Host(configuration["EventBusSettings:HostAddress"]);
            //    });
            //});
            //services.AddMassTransitHostedService();

            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Basket.API", Version = "v1" });
            });


            return services;
        }
        public static IApplicationBuilder WebApiConfigApplication(this WebApplication app, IWebHostEnvironment env)
        {

            // Configure the HTTP request pipeline.
           

            app.MapControllers();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Basket.API v1"));
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
