using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Domain.Volunteers.ValueObjects;

namespace PetFamily.Application.Dtos.Validators;
public class SocialMediaDtoValidator : AbstractValidator<SocialMediaDto>
{
    public SocialMediaDtoValidator()
    {
        RuleFor(sm => new { sm.Url, sm.Type })
            .MustBeValueObject(sm => SocialMedia.Create(sm.Type, sm.Url));
    }
}