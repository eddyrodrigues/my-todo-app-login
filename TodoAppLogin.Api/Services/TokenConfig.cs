using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TodoAppLogin.Domain.Entities;
using TodoAppLogin.Infra.Extensions;

namespace TodoAppLogin.Api.Services;

public static class TokenConfig{
  public static string? JwtKey { get; set; }
  
  public static string GenerateToken(User user)
  {

    
    var tokenHandler = new JwtSecurityTokenHandler();
    var key = Encoding.ASCII.GetBytes(JwtKey);
    var claims = RoleClaimsExtension.GetClaims(user);
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