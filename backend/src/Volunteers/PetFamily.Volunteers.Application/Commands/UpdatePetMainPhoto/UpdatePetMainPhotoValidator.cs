using FluentValidation;
using PetFamily.Core.Validation;
using PetFamily.Volunteers.Domain.ValueObjects;

namespace PetFamily.Volunteers.Application.Commands.UpdatePetMainPhoto;
public class UpdatePetMainPhotoValidator : AbstractValidator<UpdatePetMainPhotoCommand>
{
    public UpdatePetMainPhotoValidator()
    {
        RuleFor(p => p.FileName)
            .MustBeValueObject(Photo.Create);
    }
}
