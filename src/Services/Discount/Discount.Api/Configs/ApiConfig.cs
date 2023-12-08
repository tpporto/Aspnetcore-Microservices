using Discount.Api.Repositories;
using Microsoft.OpenApi.Models;

namespace Discount.Api.Configs
{
    public static class ApiConfig
    {
        public static IServiceCollection WebApiConfigService(this IServiceCollection services, IConfiguration configuration)
        {
            // Add services to the container.
            services.AddScoped<IDiscountRepository, DiscountRepository>();

            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Discount.API", Version = "v1" });
            });


            return services;
        }
        public static IApplicationBuilder WebApiConfigApplication(this WebApplication app, IWebHostEnvironment env)
        {

            // Configure the HTTP request pipeline.
           
            app.UseAuthorization();

            app.MapControllers();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Discount.API v1"));
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
