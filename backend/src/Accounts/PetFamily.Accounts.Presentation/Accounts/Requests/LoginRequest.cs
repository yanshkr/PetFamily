using PetFamily.Accounts.Application.Commands.Login;

namespace PetFamily.Accounts.Presentation.Accounts.Requests;
public record LoginRequest(
    string Email,
    string Password)
{
    public LoginCommand ToCommand() => new(Email, Password);
}