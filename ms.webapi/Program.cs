using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ms.infrastructure.protos;
using ms.webapi;

namespace ms.WebAPI
{
    public partial class Program
    {
        public static async Task Main(string[] args) // Add 'args' as a parameter
        {

            var builder = WebApplication.CreateBuilder(args);
            bool isDevelopment = builder.Environment.IsDevelopment();

            // Add grpc to the builder
            builder.RegisterGrpc(isDevelopment);

            // 注册自定义的 Kafka 日志服务
            builder.Logging.ClearProviders();
            //information以上可以自己控制
            builder.Logging.AddProvider(new KafkaLoggerProvider("localhost:9092", "my-log-topic", LogLevel.Debug));
            builder.Logging.AddFilter<KafkaLoggerProvider>(null, LogLevel.Warning);
            builder.Logging.AddFilter<KafkaLoggerProvider>("ms", LogLevel.Debug);
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            // 添加 Swagger 生成服務
            builder.Services.AddSwaggerGen(options =>
            {
                // 配置 JWT Token 的輸入
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "輸入格式為: Bearer {token}"
                });

                // 添加全局的 JWT Token 驗證
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            builder.Services.AddDbContext<MicroServiceDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            var secrect = Encoding.UTF8.GetBytes("a12d24caac19f83406fc9458469c0180");
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "Gino",
                    ValidAudience = "MSClient",
                    IssuerSigningKey = new SymmetricSecurityKey(secrect)
                };
            });

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
        }
    }

    public static class BuilderExtension
    {

        /// <summary>
        /// RegisterGrpc
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static WebApplicationBuilder RegisterGrpc(this WebApplicationBuilder builder, bool isDevelopment)
        {
            var grpcSection = isDevelopment ? "GrpcServiceDev" : "GrpcService";
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

}
