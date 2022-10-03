namespace TodoAppLogin.Domain.Entities;

public class User: Entity
{
  public User() { }
  public virtual string Name { get; set; } = string.Empty;
  public virtual string Email { get; set; } = string.Empty;
  public virtual string Password { get; set; } = string.Empty;
  public virtual List<Roles> Roles { get; set; } = new List<Roles>();
}