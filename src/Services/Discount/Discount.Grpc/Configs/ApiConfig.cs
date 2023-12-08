using Discount.Grpc.Services;

namespace Discount.Grpc.Configs
{
    public static class ApiConfig
    {
        public static IServiceCollection WebApiConfigService(this IServiceCollection services, IConfiguration configuration)
        {
            // Add services to the container.
            services.AddGrpc();


            return services;
        }
        public static IApplicationBuilder WebApiConfigApplication(this WebApplication app, IWebHostEnvironment env)
        {

            // Configure the HTTP request pipeline.
            // Configure the HTTP request pipeline.
            app.MapGrpcService<GreeterService>();
            app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

            return app;
        }

    }
}
