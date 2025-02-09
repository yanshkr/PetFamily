using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Domain.Volunteers.ValueObjects;

namespace PetFamily.Application.Dtos.Validators;
public class AddressDtoValidator : AbstractValidator<AddressDto>
{
    public AddressDtoValidator()
    {
        RuleFor(a => new { a.Country, a.State, a.City, a.Street, a.ZipCode })
            .MustBeValueObject(a => Address.Create(a.Country, a.State, a.City, a.Street, a.ZipCode));
    }
}