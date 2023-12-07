var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


Basket.Api.Configs.ApiConfig.WebApiConfigService(builder.Services, builder.Configuration);

var app = builder.Build();


Basket.Api.Configs.ApiConfig.WebApiConfigApplication(app, builder.Environment);



app.Run();
