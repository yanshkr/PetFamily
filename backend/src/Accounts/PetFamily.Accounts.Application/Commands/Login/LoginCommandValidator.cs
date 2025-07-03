using FluentValidation;

namespace PetFamily.Accounts.Application.Commands.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(l => l.Email)
            .NotEmpty();

        RuleFor(l => l.Password)
            .NotEmpty();
    }
}