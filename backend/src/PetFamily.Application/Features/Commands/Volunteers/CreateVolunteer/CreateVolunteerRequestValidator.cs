using FluentValidation;
using PetFamily.Application.Dtos.Validators;
using PetFamily.Application.Validation;
using PetFamily.Domain.Volunteers.ValueObjects;

namespace PetFamily.Application.Features.Commands.Volunteers.CreateVolunteer;
public class CreateVolunteerRequestValidator : AbstractValidator<CreateVolunteerCommand>
{
    public CreateVolunteerRequestValidator()
    {
        RuleFor(x => x.FullName)
            .SetValidator(new FullNameDtoValidator());

        RuleFor(x => x.Email)
            .MustBeValueObject(Email.Create);

        RuleFor(x => x.Description)
            .MustBeValueObject(Description.Create);

        RuleFor(x => x.Experience)
            .MustBeValueObject(ExperienceYears.Create);

        RuleFor(x => x.PhoneNumber)
            .MustBeValueObject(PhoneNumber.Create);
    }
}
