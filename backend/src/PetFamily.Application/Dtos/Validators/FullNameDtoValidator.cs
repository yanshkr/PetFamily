using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Domain.Volunteers.ValueObjects;

namespace PetFamily.Application.Dtos.Validators;
public class FullNameDtoValidator : AbstractValidator<FullNameDto>
{
    public FullNameDtoValidator()
    {
        RuleFor(fn => new { fn.FirstName, fn.MiddleName, fn.Surname })
            .MustBeValueObject(fn => FullName.Create(fn.FirstName, fn.MiddleName, fn.Surname));
    }
}
