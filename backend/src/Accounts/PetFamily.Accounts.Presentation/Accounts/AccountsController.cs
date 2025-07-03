using Microsoft.AspNetCore.Mvc;
using PetFamily.Accounts.Application.Commands.Login;
using PetFamily.Accounts.Application.Commands.Register;
using PetFamily.Accounts.Presentation.Accounts.Requests;
using PetFamily.Framework.Extensions;

namespace PetFamily.Accounts.Presentation.Accounts;

[ApiController]
[Route("[controller]")]
public class AccountsController : ControllerBase
{
    [HttpPost("registration")]
    public async Task<IActionResult> Register(
        [FromBody] RegisterRequest request,
        [FromServices] RegisterHandler registerHandler,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand();
        var result = await registerHandler.HandleAsync(command, cancellationToken);

        return result.ToResponse();
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginRequest request,
        [FromServices] LoginHandler loginHandler,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand();
        var result = await loginHandler.HandleAsync(command, cancellationToken);

        return result.ToResponse();
    }
}