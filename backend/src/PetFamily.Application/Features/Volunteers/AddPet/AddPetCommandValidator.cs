using FluentValidation;
using PetFamily.Application.Dtos.Validators;
using PetFamily.Application.Validation;
using PetFamily.Domain.Shared.ValueObjects;
using PetFamily.Domain.Volunteers.ValueObjects;

namespace PetFamily.Application.Features.Volunteers.AddPet;
public class AddPetCommandValidator : AbstractValidator<AddPetCommand>
{
    public AddPetCommandValidator()
    {
        RuleFor(p => p.Name)
            .MustBeValueObject(Name.Create);

        RuleFor(p => p.Description)
            .MustBeValueObject(Description.Create);

        RuleFor(p => p.Color)
            .MustBeValueObject(Color.Create);

        RuleFor(p => p.Address)
            .SetValidator(new AddressDtoValidator());

        RuleFor(p => p.HealthInfo)
            .MustBeValueObject(HealthInfo.Create);

        RuleFor(p => p.PhoneNumber)
            .MustBeValueObject(PhoneNumber.Create);

        RuleFor(p => p.Weight)
            .MustBeValueObject(WeightMeasurement.CreateFromGrams);

        RuleFor(p => p.Height)
            .MustBeValueObject(HeightMeasurement.CreateFromCentimeters);
    }
}
