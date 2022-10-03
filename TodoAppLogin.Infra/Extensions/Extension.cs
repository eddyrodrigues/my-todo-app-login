using System.Security.Claims;
using TodoAppLogin.Domain.Entities;

namespace TodoAppLogin.Infra.Extensions;

public static class RoleClaimsExtension
{
    public static IEnumerable<Claim> GetClaims(this User user)
    {
            var result = new List<Claim>
          {
              new(ClaimTypes.Name, user.Name),
              new(ClaimTypes.Email, user.Email),
              new Claim("UserId", user.Id.ToString())
          };
        result.AddRange(  
            user.Roles.Select(role => new Claim(ClaimTypes.Role, role.Name))
        );
        return result;
    }
}