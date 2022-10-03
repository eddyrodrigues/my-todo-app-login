using FluentValidation;
using TodoApp.Infra.CommandsRequest;

namespace TodoAppLogin.Infra.CommandsValidator;

public class CreateTokenCommandRequestValidator : AbstractValidator<CreateTokenCommandRequest>
{
  public CreateTokenCommandRequestValidator()
  {
    RuleFor(o => o.Email).NotNull().EmailAddress().Length(1, 255).WithMessage("Description should have between 1 and 255 chars");
    RuleFor(o => o.Password).NotNull().Length(1, 255).WithMessage("Description should hava between 1 and 255 chars");
  }
}