using Microsoft.EntityFrameworkCore;
using ms.infrastructure.protos;
using ms.infrastructure.System.BuilderExtension;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

//builder.RegisterKafkaLogger();
builder.RegisterGrpc();
builder.RegisterDBConnection();
builder.RegisterJWTTokenAuth();
builder.RegisterSwaggerWithJTWToken();

builder.Services.AddAuthorization();

var app = builder.Build();

// ✅ 執行 migration：確保資料庫存在，並套用最新 migration
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<MicroServiceDbContext>();

    // 若要用 migration 系統（推薦），使用這行
    dbContext.Database.Migrate();
}

// // Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || true)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
/* 先掛一個極簡 healthz（純文字 200 OK） */
app.MapGet("/healthz", () => Results.Text("OK"))
   .AllowAnonymous();      // 若全域有 RequireAuthorization，可確保此路徑不需登入

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


