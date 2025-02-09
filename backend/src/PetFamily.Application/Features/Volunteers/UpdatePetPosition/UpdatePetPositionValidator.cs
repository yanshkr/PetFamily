using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Domain.Volunteers.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Features.Volunteers.UpdatePetPosition;
public class UpdatePetPositionValidator : AbstractValidator<UpdatePetPositionCommand>
{
    public UpdatePetPositionValidator()
    {
        RuleFor(p => p.Position)
            .MustBeValueObject(PetPosition.Create);
    }
}
