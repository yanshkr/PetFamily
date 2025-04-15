using FluentValidation;
using PetFamily.Core.Validation;
using PetFamily.Volunteers.Contracts.Dtos.Volunteer;
using PetFamily.Volunteers.Domain.ValueObjects;

namespace PetFamily.Volunteers.Application.Validators;
public class SocialMediaDtoValidator : AbstractValidator<SocialMediaDto>
{
    public SocialMediaDtoValidator()
    {
        RuleFor(sm => new { sm.Url, sm.Name })
            .MustBeValueObject(sm => SocialMedia.Create(sm.Name, sm.Url));
    }
}