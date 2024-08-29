using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ms.infrastructure.protos;
using ms.webapi.Models;

namespace ms.webapi.Controllers
{
  [Authorize]
  [ApiController]
  [Route("api/[controller]")]
  public class UserController : ControllerBase
  {
    private readonly UserProto.UserProtoClient _client;
    public UserController(UserProto.UserProtoClient client)
    {
      _client = client;
    }
    [AllowAnonymous]
    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginRequest instance)
    {
      StandardResponseDto<string> response = new StandardResponseDto<string>();
      try
      {
        LoginRequest requst = new LoginRequest()
        {
          Email = instance.Email,
          Mima = instance.Mima
        };

        var reply = await _client.LoginAsync(requst);
        response.is_success = reply.Result.IsSuccess;
        response.msg = reply.Result.Msg;
        response.data = response.is_success ? reply.Data : null;
      }
      catch (Exception ex)
      {

      }

      return Ok(response);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
      StandardResponseDto<UserResponseDto> response = new StandardResponseDto<UserResponseDto>();
      try
      {

        GetUserByIdRequest requst = new GetUserByIdRequest()
        {
          Id = id
        };

        var reply = await _client.GetUserByIdAsync(requst);
        response.is_success = reply.Result.IsSuccess;
        response.msg = reply.Result.Msg;

        if (response.is_success)
        {
          response.data = new UserResponseDto()
          {
            user_id = reply.Data.UserId,
            name = reply.Data.Name,
            email = reply.Data.Email,
            is_enable = reply.Data.IsEnable,
            last_login_at = (reply.Data.LastLoginAt == null) ? null : reply.Data.LastLoginAt.ToDateTime(),
            mima_change_at = (reply.Data.MimaChangeAt == null) ? null : reply.Data.MimaChangeAt.ToDateTime(),
            create_at = (reply.Data.CreateAt == null) ? null : reply.Data.CreateAt.ToDateTime(),
            update_at = (reply.Data.UpdateAt == null) ? null : reply.Data.UpdateAt.ToDateTime(),
          };
        }

      }
      catch (Exception ex)
      {

      }

      return Ok(response);
    }
    [HttpGet("GetUserByQuery")]
    public async Task<IActionResult> GetUserByQuery(GetUserByQueryRequest instance)
    {
      var reply = await _client.GetUserByQueryAsync(instance);

      return Ok(reply);
    }
    [HttpPost("RegisterUser")]
    public async Task<IActionResult> RegisterUser(RegisterUserRequestDto instance)
    {
      StandardResponseDto<string> response = new StandardResponseDto<string>();
      try
      {
        var request = new RegisterUserRequest()
        {
          Name = instance.name,
          Email = instance.email,
          Mima = instance.mima
        };
        var reply = await _client.RegisterUserAsync(request);
        response.is_success = reply.Result.IsSuccess;
        response.msg = reply.Result.Msg;
        response.data = null;
      }
      catch (Exception ex)
      {

      }

      return Ok(response);
    }
    [HttpPost("EnableUser")]
    public async Task<IActionResult> EnableUser(int id)
    {
      StandardResponseDto<string> response = new StandardResponseDto<string>();
      try
      {
        var request = new GetUserByIdRequest() { Id = id };
        var reply = await _client.EnableUserAsync(request);
        response.is_success = reply.Result.IsSuccess;
        response.msg = reply.Result.Msg;
        response.data = null;
      }
      catch (Exception ex)
      {

        throw;
      }

      return Ok(response);
    }
    [HttpPost("DisableUser")]
    public async Task<IActionResult> DisableUser(int id)
    {
      StandardResponseDto<string> response = new StandardResponseDto<string>();
      try
      {
        var request = new GetUserByIdRequest() { Id = id };
        var reply = await _client.DisableUserAsync(request);
        response.is_success = reply.Result.IsSuccess;
        response.msg = reply.Result.Msg;
        response.data = null;
      }
      catch (Exception ex)
      {

        throw;
      }

      return Ok(response);
    }
  }
}
