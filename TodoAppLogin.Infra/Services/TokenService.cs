using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TodoAppLogin.Domain.Entities;
using TodoAppLogin.Infra.Extensions;

namespace TodoAppLogin.Web.Services;

public static class TokenService {
  public static string JwtKey { get; set; } = string.Empty;

  public static string GenerateToken(User user)
  {

    
    var tokenHandler = new JwtSecurityTokenHandler();
    var key = Encoding.ASCII.GetBytes(JwtKey);
    // var claims = RoleClaimsExtension.GetClaims(user);
    var claims = user.GetClaims();
    var tokenDescriptor = new SecurityTokenDescriptor
    {
        Subject = new ClaimsIdentity(claims),
        Expires = DateTime.UtcNow.AddHours(8),
        SigningCredentials = new SigningCredentials(
            new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha256Signature)
    };
    var token = tokenHandler.CreateToken(tokenDescriptor);
    return tokenHandler.WriteToken(token);
  }

}