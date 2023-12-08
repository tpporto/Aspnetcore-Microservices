using Discount.Grpc.Services;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682
Discount.Grpc.Configs.ApiConfig.WebApiConfigService(builder.Services, builder.Configuration);

var app = builder.Build();


Discount.Grpc.Configs.ApiConfig.WebApiConfigApplication(app, builder.Environment);



app.Run();
