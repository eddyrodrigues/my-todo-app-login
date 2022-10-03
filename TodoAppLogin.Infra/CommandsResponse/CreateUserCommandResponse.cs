using TodoApp.Infra.CommandsResponse;

namespace TodoAppLogin.Infra.CommandsResponse;

public class CreateUserCommandResponse
{
  public CreateUserCommandResponse(string email, string name, string accessToken)
  {
    Email = email;
    Name = name;
    AccessToken = accessToken;
  }

  public string Email { get; set; }
  public string Name { get; set; }
  public string AccessToken { get; set; }
  
}