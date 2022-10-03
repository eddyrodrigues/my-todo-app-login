using Flunt.Notifications;
using TodoAppLogin.Infra.Commands;

namespace TodoApp.Infra.CommandsResponse;

public class CreateTodoItemCommandResponse : Notifiable<Notification>, ICommandResult
{
  public CreateTodoItemCommandResponse(Guid id, string title, string description)
  {
    Id = id;
    Title = title;
    Description = description;
  }

  public Guid Id { get; private set; }
  public string Title { get; private set; }
  public string Description { get; private set; }
}