using Catalog.Api.Configs;

var builder = WebApplication.CreateBuilder(args);


Catalog.Api.Configs.ApiConfig.WebApiConfigService(builder.Services, builder.Configuration);

var app = builder.Build();


Catalog.Api.Configs.ApiConfig.WebApiConfigApplication(app, builder.Environment);


app.Run();
