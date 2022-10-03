namespace TodoAppLogin.Infra.Commands;

public interface ICommandHandler<T>
  where T : ICommand
{
  ICommandResult handle(T command);
}