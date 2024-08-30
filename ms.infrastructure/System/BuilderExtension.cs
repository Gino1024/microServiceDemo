using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace ms.infrastructure.System.BuilderExtension
{
  public static class BuilderExtension
  {

    /// <summary>
    /// RegisterKafkaLogger
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static WebApplicationBuilder RegisterKafkaLogger(this WebApplicationBuilder builder)
    {
      builder.Logging.ClearProviders();
      builder.Logging.AddProvider(new KafkaLoggerProvider("localhost:9092", "my-log-topic", LogLevel.Debug));
      builder.Logging.AddFilter<KafkaLoggerProvider>(null, LogLevel.Warning);
      builder.Logging.AddFilter<KafkaLoggerProvider>("ms", LogLevel.Debug);
      return builder;
    }
    /// <summary>
    /// RegisterDBConn
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static WebApplicationBuilder RegisterDBConnection(this WebApplicationBuilder builder)
    {
      builder.Services.AddDbContext<MicroServiceDbContext>(options =>
          options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


      return builder;

    }
    /// <summary>
    /// RegisterJWTTokenAuth
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static WebApplicationBuilder RegisterJWTTokenAuth(this WebApplicationBuilder builder)
    {
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

      return builder;
    }

    public static WebApplicationBuilder RegisterSwaggerWithJTWToken(this WebApplicationBuilder builder)
    {

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

      return builder;
    }
  }
}