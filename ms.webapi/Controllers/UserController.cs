using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using ms.infrastructure.protos;

namespace ms.webapi.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class UserController : ControllerBase
  {
    private readonly User.UserClient _client;
    public UserController(User.UserClient client)
    {
      _client = client;
    }
    // GET: api/user
    [HttpGet]
    public async Task<IActionResult> Get()
    {
      var reply = await _client.GetListAsync(
                    new HelloRequest { Name = "World" });
      Console.WriteLine("Greeting: " + reply.Message);

      // TODO: Implement logic to retrieve all users
      return Ok(reply.Message);
    }

  }
}
