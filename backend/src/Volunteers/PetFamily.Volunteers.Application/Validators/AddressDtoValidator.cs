using FluentValidation;
using PetFamily.Core.Validation;
using PetFamily.Volunteers.Contracts.Dtos.Pet;
using PetFamily.Volunteers.Domain.ValueObjects;

namespace PetFamily.Volunteers.Application.Validators;
public class AddressDtoValidator : AbstractValidator<AddressDto>
{
    public AddressDtoValidator()
    {
        RuleFor(a => new { a.Country, a.State, a.City, a.Street, a.ZipCode })
            .MustBeValueObject(a => Address.Create(a.Country, a.State, a.City, a.Street, a.ZipCode));
    }
}