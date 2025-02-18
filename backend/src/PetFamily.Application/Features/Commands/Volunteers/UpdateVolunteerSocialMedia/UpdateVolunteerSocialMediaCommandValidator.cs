using FluentValidation;
using PetFamily.Application.Dtos.Validators;

namespace PetFamily.Application.Features.Commands.Volunteers.UpdateVolunteerSocialMedia;
public class UpdateVolunteerSocialMediaCommandValidator : AbstractValidator<UpdateVolunteerSocialMediaCommand>
{
    public UpdateVolunteerSocialMediaCommandValidator()
    {
        RuleForEach(us => us.SocialMedias)
            .SetValidator(new SocialMediaDtoValidator());
    }
}
