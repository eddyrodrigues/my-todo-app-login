using TodoAppLogin.Infra.Commands;

namespace TodoApp.Infra.CommandsResponse;

public class GenericCommandResult : ICommandResult
{
  public GenericCommandResult() { }

  public GenericCommandResult(bool success, string message, object data)
  {
    Success = success;
    Message = new List<string>();
    Message.Add(message);
    Data = data;
  }
  public GenericCommandResult(bool success, string message)
  {
    Success = success;
    Message = new List<string>();
    Message.Add(message);
  }
  public GenericCommandResult(bool success, IList<string> message, object data)
  {
    Success = success;
    Message = new List<string>();
    Message.AddRange(message);
    Data = data;
  }
  public GenericCommandResult(bool success, IList<string> message)
  {
    Success = success;
    Message = new List<string>();
    Message.AddRange(message);
  }



  public bool Success { get; set; }
  public List<string> Message { get; set; } = new List<string>();
  public object? Data { get; set; }
}