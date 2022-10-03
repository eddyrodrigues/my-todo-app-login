namespace TodoAppLogin.Domain.Entities;

public class Roles : Entity
{
  public string Name { get; set; }
  public List<User> Users { get; set; } = new List<User>();
}