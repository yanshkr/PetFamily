using FluentValidation;
using PetFamily.Core.Validation;
using PetFamily.Volunteers.Domain.ValueObjects;

namespace PetFamily.Volunteers.Application.Commands.UpdateVolunteerMainInfo;
public class UpdateVolunteerCommandValidator : AbstractValidator<UpdateVolunteerCommand>
{
    public UpdateVolunteerCommandValidator()
    {
        RuleFor(x => new { x.FirstName, x.MiddleName, x.Surname })
            .MustBeValueObject(f => FullName.Create(f.FirstName, f.MiddleName, f.Surname));

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
