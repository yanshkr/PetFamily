using FluentValidation;
using PetFamily.Core.Validation;
using PetFamily.Volunteers.Domain.ValueObjects;

namespace PetFamily.Volunteers.Application.Commands.UpdatePetPosition;
public class UpdatePetPositionValidator : AbstractValidator<UpdatePetPositionCommand>
{
    public UpdatePetPositionValidator()
    {
        RuleFor(p => p.Position)
            .MustBeValueObject(PetPosition.Create);
    }
}
