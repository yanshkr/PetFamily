using FluentValidation;
using PetFamily.Application.Features.Volunteers.UpdateVolunteerSocialMedia.Contracts;
using PetFamily.Application.Validation;
using PetFamily.Domain.ValueObjects;

namespace PetFamily.Application.Features.Volunteers.UpdateVolunteerSocialMedia;
public class UpdateVolunteerPaymentInfoDtoValidator : AbstractValidator<UpdateVolunteerSocialMediaDto>
{
    public UpdateVolunteerPaymentInfoDtoValidator()
    {
        RuleForEach(x => x.SocialMedias)
            .MustBeValueObject(s => SocialMedia.Create(s.Type, s.Url));
    }
}
