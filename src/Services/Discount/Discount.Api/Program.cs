var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

Discount.Api.Configs.ApiConfig.WebApiConfigService(builder.Services, builder.Configuration);

var app = builder.Build();


Discount.Api.Configs.ApiConfig.WebApiConfigApplication(app, builder.Environment);

Discount.Api.Extensions.HostExtensions.MigrateDatabase<Program>(app);
app.Run();
