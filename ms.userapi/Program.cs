using ms.user.Services;
using Repository;
using UserInfra.Repository;
using ms.infrastructure.System.BuilderExtension;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();

builder.RegisterService();
builder.RegisterKafkaLogger();
builder.RegisterDBConnection();
builder.RegisterJWTTokenAuth();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<UserGrpcService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();



public static class BuilderExtension
{
    public static WebApplicationBuilder RegisterService(this WebApplicationBuilder builder)
    {

        builder.Services.AddScoped<IUniOfWork, UniOfWork>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();

        return builder;
    }
}

/// <summary>
/// GrpcServiceRouteConfig
/// </summary>
public class GrpcServiceConfig
{
    public string Name { get; set; }
    public string Url { get; set; }
}