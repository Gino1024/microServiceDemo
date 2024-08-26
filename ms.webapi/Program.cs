using Microsoft.EntityFrameworkCore;
using ms.infrastructure.protos;
using ms.webapi;

namespace ms.WebAPI
{
    public partial class Program
    {
        public static async Task Main(string[] args) // Add 'args' as a parameter
        {

            var builder = WebApplication.CreateBuilder(args);

            // Add grpc to the builder
            builder.RegisterGrpc();

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<MicroServiceDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            var app = builder.Build();

            // // Configure the HTTP request pipeline.
            // if (app.Environment.IsDevelopment())
            // {
            //     app.UseSwagger();
            //     app.UseSwaggerUI();
            // }

            app.UseSwagger();
            app.UseSwaggerUI();

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
        public static WebApplicationBuilder RegisterGrpc(this WebApplicationBuilder builder)
        {
            var grpcServices = builder.Configuration.GetSection("GrpcService").Get<List<GrpcServiceConfig>>();
            var userServiceUrl = grpcServices?.First(service => service.Name == "User").Url;

            builder.Services.AddGrpcClient<UserProto.UserProtoClient>(o =>
                    {
                        o.Address = new Uri(userServiceUrl);
                    });

            return builder;
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
}