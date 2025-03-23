using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Domain.Volunteers.ValueObjects;

namespace PetFamily.Application.Dtos.Validators;
public class SocialMediaDtoValidator : AbstractValidator<SocialMediaDto>
{
    public SocialMediaDtoValidator()
    {
        RuleFor(sm => new { sm.Url, sm.Name })
            .MustBeValueObject(sm => SocialMedia.Create(sm.Name, sm.Url));
    }
}