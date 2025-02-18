using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Domain.Shared.ValueObjects;

namespace PetFamily.Application.Features.Commands.Species.Create;
public class CreateSpecieCommandValidator : AbstractValidator<CreateSpecieCommand>
{
    public CreateSpecieCommandValidator()
    {
        RuleFor(x => x.Name)
            .MustBeValueObject(Name.Create);
    }
}
