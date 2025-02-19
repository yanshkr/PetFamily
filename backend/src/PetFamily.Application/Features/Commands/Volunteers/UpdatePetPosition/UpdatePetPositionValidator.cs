using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Domain.Volunteers.ValueObjects;

namespace PetFamily.Application.Features.Commands.Volunteers.UpdatePetPosition;
public class UpdatePetPositionValidator : AbstractValidator<UpdatePetPositionCommand>
{
    public UpdatePetPositionValidator()
    {
        RuleFor(p => p.Position)
            .MustBeValueObject(PetPosition.Create);
    }
}
