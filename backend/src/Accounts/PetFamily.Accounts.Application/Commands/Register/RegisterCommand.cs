using PetFamily.Core.Abstraction;

namespace PetFamily.Accounts.Application.Commands.Register;
public record RegisterCommand(string Username, string Email, string Password) : ICommand;