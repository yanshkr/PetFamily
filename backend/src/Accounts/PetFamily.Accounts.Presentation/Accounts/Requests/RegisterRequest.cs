using PetFamily.Accounts.Application.Commands.Login;
using PetFamily.Accounts.Application.Commands.Register;

namespace PetFamily.Accounts.Presentation.Accounts.Requests;
public record RegisterRequest(
    string Username,
    string Email,
    string Password)
{
    public RegisterCommand ToCommand() => new(Username, Email, Password);
}