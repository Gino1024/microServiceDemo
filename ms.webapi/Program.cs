using ms.infrastructure.protos;
using ms.infrastructure.System.BuilderExtension;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.RegisterKafkaLogger();
builder.RegisterGrpc();
builder.RegisterDBConnection();
builder.RegisterJWTTokenAuth();
builder.RegisterSwaggerWithJTWToken();

builder.Services.AddAuthorization();

var app = builder.Build();

// // Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || true)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
public static class BuilderExtension
{

    /// <summary>
    /// RegisterGrpc
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static WebApplicationBuilder RegisterGrpc(this WebApplicationBuilder builder)
    {
        var grpcSection = "GrpcService";
        var grpcServices = builder.Configuration.GetSection(grpcSection).Get<List<GrpcServiceConfig>>();
        var userServiceUrl = grpcServices?.First(service => service.Name == "User").Url;

        builder.Services.AddGrpcClient<UserProto.UserProtoClient>(o =>
        {
            o.Address = new Uri(userServiceUrl);
        });

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


