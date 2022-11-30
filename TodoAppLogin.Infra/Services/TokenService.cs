using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TodoAppLogin.Domain.Entities;
using TodoAppLogin.Infra.Extensions;

namespace TodoAppLogin.Web.Services;

public static class TokenService {
  private static string JwtKey { get; set; } = string.Empty;

  /// <summary>
  /// ArgumentNullException
  /// </summary>
  /// <param name="jwtTokenKeyEsperado"></param>
  public static void SetJwtKey(string? jwtTokenKeyEsperado)
  {
    if (jwtTokenKeyEsperado is null)
    {
      throw new ArgumentNullException(nameof(jwtTokenKeyEsperado));
    }
    JwtKey = jwtTokenKeyEsperado;
  }
  public static string GetJwtKey => JwtKey;
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