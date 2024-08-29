using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ms.infrastructure.JWTHandler
{
  public static class JWTHandler
  {
    private static readonly string ms_key = "a12d24caac19f83406fc9458469c0180";
    public static string GenerateJwtToken(string identifier)
    {
      var claims = new[]
      {
        new Claim(JwtRegisteredClaimNames.Sub, identifier),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ms_key));
      var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

      var token = new JwtSecurityToken(
          issuer: "Gino",
          audience: "MSClient",
          claims: claims,
          expires: DateTime.Now.AddMinutes(30),
          signingCredentials: creds);

      return new JwtSecurityTokenHandler().WriteToken(token);
    }
  }
}