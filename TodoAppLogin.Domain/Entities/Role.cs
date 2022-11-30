namespace TodoAppLogin.Domain.Entities;

public class Roles : Entity
{
  public Roles(string name)
  {
    Name = name;
  }
  public string Name { get; set; }
  public List<User> Users { get; set; } = new List<User>();
}