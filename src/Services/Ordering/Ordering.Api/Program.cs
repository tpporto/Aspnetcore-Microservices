using Ordering.Api.Extensions;
using Ordering.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Ordering.Api.Configs.ApiConfig.WebApiConfigService(builder.Services, builder.Configuration);

var app = await builder.Build()
               .MigrateDatabase<OrderContext>((context, services) =>
                {
                    var logger = services.GetService<ILogger<OrderContextSeed>>();
                    OrderContextSeed
                        .SeedAsync(context, logger)
                        .Wait();
                })
               ;

Ordering.Api.Configs.ApiConfig.WebApiConfigApplication(app, builder.Environment);

app.Run();
