using Discount.Grpc.Repositories;
using Discount.Grpc.Services;
using System.Reflection;

namespace Discount.Grpc.Configs
{
    public static class ApiConfig
    {
        public static IServiceCollection WebApiConfigService(this IServiceCollection services, IConfiguration configuration)
        {
            // Add services to the container.
            services.AddAutoMapper(Assembly.GetEntryAssembly());
            services.AddScoped<IDiscountRepository, DiscountRepository>();
            services.AddGrpc();

            return services;
        }
        public static IApplicationBuilder WebApiConfigApplication(this WebApplication app, IWebHostEnvironment env)
        {

            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            //app.MapGrpcService<DiscountService>();
            //app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
           
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<DiscountService>();

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                });
            });
            return app;
        }

    }
}
