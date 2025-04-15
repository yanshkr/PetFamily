using FluentValidation;
using PetFamily.Volunteers.Application.Validators;

namespace PetFamily.Volunteers.Application.Commands.UpdateVolunteerSocialMedia;
public class UpdateVolunteerSocialMediaCommandValidator : AbstractValidator<UpdateVolunteerSocialMediaCommand>
{
    public UpdateVolunteerSocialMediaCommandValidator()
    {
        RuleForEach(us => us.SocialMedias)
            .SetValidator(new SocialMediaDtoValidator());
    }
}
