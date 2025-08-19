using ms.user.Services;
using Repository;
using UserInfra.Repository;
using ms.infrastructure.System.BuilderExtension;
using Grpc.HealthCheck;
using Grpc.Health.V1;
using Microsoft.Extensions.Diagnostics.HealthChecks;



var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddGrpcHealthChecks()
    .AddCheck("self", () => HealthCheckResult.Healthy());

builder.RegisterService();
//builder.RegisterKafkaLogger();
builder.RegisterDBConnection();
builder.RegisterJWTTokenAuth();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<UserGrpcService>();
app.MapGrpcHealthChecksService(); // 這行會提供標準 gRPC Health API

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