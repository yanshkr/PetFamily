using FluentValidation;
using PetFamily.Core.Validation;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.Volunteers.Application.Validators;
using PetFamily.Volunteers.Domain.ValueObjects;

namespace PetFamily.Volunteers.Application.Commands.AddPet;
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
