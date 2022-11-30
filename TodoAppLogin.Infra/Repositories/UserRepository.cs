using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TodoAppLogin.Domain.Dto;
using TodoAppLogin.Domain.Entities;
using TodoAppLogin.Infra.Context;

namespace TodoAppLogin.Infra.Repositories;

public class UserRepository
{
  private readonly UserDbContext _context;

  public UserRepository(UserDbContext _context)
  {
    this._context = _context;
  }

  public User GetById(int id)
  {
    return _context.User.Find(id) ?? new User();
  }
  public User? GetByEmail(string email, Expression<Func<User, User>> fields)
  {
    var user =_context.User
      .AsNoTracking()
      .Where(u => u.Email == email)
      .Select(fields)
    .FirstOrDefault();

    return user;
  }

  public User? GetByEmail(string email)
  {
    var user =_context.User.AsNoTracking().Where(u => u.Email == email).FirstOrDefault();
    return user;
  }
  public User GetByEmailToGenerateToken(string email)
  {
    var user = _context.User.AsNoTracking().Where(u => u.Email == email).Select(u => new User()
    {
      Name = u.Name,
      Password = u.Password,
      Email = u.Email,
      Roles = u.Roles,
      Id = u.Id
    }).FirstOrDefault();

    return user ?? new User();
  }
  public User Add(User user)
  {
    _context.Add(user);
    _context.SaveChanges();
    return user;
  }
}