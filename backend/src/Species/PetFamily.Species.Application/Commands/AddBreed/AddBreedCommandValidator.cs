using FluentValidation;
using PetFamily.Core.Validation;
using PetFamily.SharedKernel.ValueObjects;

namespace PetFamily.Species.Application.Commands.AddBreed;
public class AddBreedCommandValidator : AbstractValidator<AddBreedCommand>
{
    public AddBreedCommandValidator()
    {
        RuleFor(x => x.Name)
            .MustBeValueObject(Name.Create);
    }
}
