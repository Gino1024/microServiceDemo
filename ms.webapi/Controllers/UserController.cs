using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using ms.infrastructure.protos;

namespace ms.webapi.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class UserController : ControllerBase
  {
    private readonly UserProto.UserProtoClient _client;
    public UserController(UserProto.UserProtoClient client)
    {
      _client = client;
    }
    // GET: api/user
    [HttpGet]
    public async Task<IActionResult> Get()
    {
      var request = new Empty();
      var reply = await _client.GetListAsync(request);

      // TODO: Implement logic to retrieve all users
      return Ok(reply);
    }
    // GET: api/user
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
      var reply = await _client.GetAsync(
                    new UserRequest() { Id = id });

      // TODO: Implement logic to retrieve all users
      return Ok(reply);
    }
  }
}
