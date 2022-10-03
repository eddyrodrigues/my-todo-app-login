using Microsoft.EntityFrameworkCore;
using SecureIdentity.Password;
using TodoApp.Infra.CommandsRequest;
using TodoApp.Infra.CommandsResponse;
using TodoAppLogin.Domain.Entities;
using TodoAppLogin.Infra.Commands;
using TodoAppLogin.Infra.CommandsResponse;
using TodoAppLogin.Infra.CommandsValidator;
using TodoAppLogin.Infra.Repositories;
using TodoAppLogin.Web.Services;

namespace TodoApp.Infra.CommandsHandler;

public class LoginCommandHandler : ICommandHandler<CreateTokenCommandRequest>
{
  private readonly UserRepository _userRepository;

  public LoginCommandHandler(UserRepository userRepository)
  {
    _userRepository = userRepository;
  }

  public ICommandResult handle(CreateTokenCommandRequest command)
  {
    var validator = new CreateTokenCommandRequestValidator().Validate(command);
    if (validator.IsValid)
    {
      var user = _userRepository.GetByEmailToGenerateToken(command.Email);
      if (PasswordHasher.Verify(user.Password ?? "", command.Password))
        return new GenericCommandResult(true, "Token gerado com sucesso", TokenService.GenerateToken(user));
        return new GenericCommandResult(false, "Usuário ou senhas incorretos");
    }
    else
    {
      return new GenericCommandResult(false, "Verifique os dados e tente novamente");
    }
  }
  public ICommandResult handle(CreateUserCommandRequest command)
  {
    var validator = new CreateUserCommandRequestValidator().Validate(command);
    if (validator.IsValid)
    {
      var user = new User(){
        Name = command.Name,
        Email = command.Email,
        Password = PasswordHasher.Hash(command.Password)

      };
      try{
        var emailExists = _userRepository.GetByEmail(user.Email);
        if (emailExists.Id > 0)
          _userRepository.Add(user);
        else
          return new GenericCommandResult(false, "Não foi possível criar o usuário, favor verificar os dados e tentar novamente");  
      }catch (Exception)
      {
        return new GenericCommandResult(false, "Não foi possível criar o usuário, favor verificar os dados e tentar novamente");
      }
      if (user.Id > 0)
      {
        return new GenericCommandResult(true, "Usuário criado com sucesso!",
          new CreateUserCommandResponse(user.Email, user.Name, TokenService.GenerateToken(user)));
      } else {
        return new GenericCommandResult(false, "Não foi possível criar o usuário, favor verificar os dados e tentar novamente");
      }
    }
    else
    {
      // return new GenericCommandResult(false,validator.Errors.Select(e => $@"{e.PropertyName}:{e.ErrorMessage}").ToList(), null);
      return new GenericCommandResult(false,"Favor verificar os dados e tentar novamente.");
    }
  }
}