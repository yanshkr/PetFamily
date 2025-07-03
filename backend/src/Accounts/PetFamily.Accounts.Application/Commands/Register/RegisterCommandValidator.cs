using FluentValidation;

namespace PetFamily.Accounts.Application.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(l => l.Username)
            .NotEmpty();
        
        RuleFor(l => l.Email)
            .NotEmpty();

        RuleFor(l => l.Password)
            .NotEmpty();
    }
}