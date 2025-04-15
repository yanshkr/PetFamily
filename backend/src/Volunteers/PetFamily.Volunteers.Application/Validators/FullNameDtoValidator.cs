using FluentValidation;
using PetFamily.Core.Validation;
using PetFamily.Volunteers.Contracts.Dtos.Volunteer;
using PetFamily.Volunteers.Domain.ValueObjects;

namespace PetFamily.Volunteers.Application.Validators;
public class FullNameDtoValidator : AbstractValidator<FullNameDto>
{
    public FullNameDtoValidator()
    {
        RuleFor(fn => new { fn.FirstName, fn.MiddleName, fn.Surname })
            .MustBeValueObject(fn => FullName.Create(fn.FirstName, fn.MiddleName, fn.Surname));
    }
}
