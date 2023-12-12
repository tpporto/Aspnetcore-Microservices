var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Ordering.Api.Configs.ApiConfig.WebApiConfigService(builder.Services, builder.Configuration);

var app = builder.Build()
              // .MigrateDatabase<OrderContext>((context, services) =>
              //  {
               //     var logger = services.GetService<ILogger<OrderContextSeed>>();
               //     OrderContextSeed
               //         .SeedAsync(context, logger)
               //         .Wait();
               // })
               ;

Ordering.Api.Configs.ApiConfig.WebApiConfigApplication(app, builder.Environment);

Ordering.Api.Extensions.HostExtensions.MigrateDatabase<Program>(app);

app.Run();
