using FluentValidation;
using TodoApp.Infra.CommandsRequest;

namespace TodoAppLogin.Infra.CommandsValidator;

public class CreateUserCommandRequestValidator : AbstractValidator<CreateUserCommandRequest>
{
  public CreateUserCommandRequestValidator()
  {
    RuleFor(o => o.Email).NotNull().EmailAddress().Length(1, 255).WithMessage("Email should have between 1 and 255 chars");
    RuleFor(o => o.Password).NotNull().Length(1, 255).WithMessage("Password should hava between 1 and 255 chars");
    RuleFor(o => o.Name).NotNull().Length(1, 255).WithMessage("Name should have between 1 and 255 chars");
  }
}