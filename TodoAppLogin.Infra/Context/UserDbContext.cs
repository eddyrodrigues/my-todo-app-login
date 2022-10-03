using Microsoft.EntityFrameworkCore;
using TodoAppLogin.Domain.Entities;
using TodoAppLogin.Infra.Map;

namespace TodoAppLogin.Infra.Context;

public class UserDbContext : DbContext
{
  public DbSet<User> User => Set<User>();

  public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) {}
  
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.ApplyConfiguration(new UserMap());
  }
}