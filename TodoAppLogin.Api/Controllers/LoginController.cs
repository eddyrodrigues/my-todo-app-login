using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Infra.CommandsHandler;
using TodoApp.Infra.CommandsRequest;

namespace TodoAppLogin.Api.Controllers;

[ApiController]
[Route("v1/[controller]")]

public class LoginController : ControllerBase
{

  private readonly ILogger<LoginController> _logger;

  // public LoginController(ILogger<LoginController> logger)
  // {
  //     _logger = logger;
  // }
  public LoginController()
  {
    
  }
  
  [AllowAnonymous]
  [HttpGet]
  [Route("/check")]
  public IActionResult check()
  {
    return Ok(new
    {
      name = "Eduardo Test",
      connect = true
    });
  }

  [AllowAnonymous]
  [HttpPost]
  [Route("/Token")]
  public IActionResult CreateToken(
    [FromServices]LoginCommandHandler loginCommandHandler,
    [FromBody] CreateTokenCommandRequest createTokenCommandRequest
    )
  {
    // Log.Information("Initialized  creating Token:");
    // _logger.LogInformation("123123", "Initialized  creating Token");
    var timer = DateTime.Now;
    var commandResult = loginCommandHandler.handle(createTokenCommandRequest);
    var elapsedTime = DateTime.Now - timer;
    Log.Information("Finalized creating Token: {Elapsed:000}  ms", elapsedTime.TotalMilliseconds);
    return Ok(commandResult);
  }
  [HttpPost]
  [Route("/NewUser")]
  public IActionResult PostCreateLogin(
    [FromServices]LoginCommandHandler loginCommandHandler,
    [FromBody] CreateUserCommandRequest createUserCommandRequest
    )
  {
    return Ok(loginCommandHandler.handle(createUserCommandRequest));
  }
}
