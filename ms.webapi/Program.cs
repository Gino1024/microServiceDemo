using ms.infrastructure.protos;
using ms.webapi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var grpcServices = builder.Configuration.GetSection("GrpcService").Get<List<GrpcServiceConfig>>();
var userServiceUrl = grpcServices?.First(service => service.Name == "User").Url;

builder.Services.AddGrpcClient<User.UserClient>(o =>
        {
            o.Address = new Uri(userServiceUrl);
        });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public class GrpcServiceConfig
{
    public string Name { get; set; }
    public string Url { get; set; }
}
