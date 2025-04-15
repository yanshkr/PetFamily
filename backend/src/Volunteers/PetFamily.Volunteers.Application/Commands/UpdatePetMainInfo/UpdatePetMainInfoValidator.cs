using FluentValidation;
using PetFamily.Core.Validation;
using PetFamily.SharedKernel.ValueObjects;
using PetFamily.Volunteers.Domain.ValueObjects;

namespace PetFamily.Volunteers.Application.Commands.UpdatePetMainInfo;
public class UpdatePetMainInfoValidator : AbstractValidator<UpdatePetMainInfoCommand>
{
    public UpdatePetMainInfoValidator()
    {
        RuleFor(p => p.Name)
            .MustBeValueObject(Name.Create);

        RuleFor(p => p.Description)
            .MustBeValueObject(Description.Create);

        RuleFor(p => p.Color)
            .MustBeValueObject(Color.Create);

        RuleFor(p => p.HealthInfo)
            .MustBeValueObject(HealthInfo.Create);

        RuleFor(p => new { p.Address.Country, p.Address.State, p.Address.City, p.Address.Street, p.Address.ZipCode })
            .MustBeValueObject(a => Address.Create(a.Country, a.State, a.City, a.Street, a.ZipCode));

        RuleFor(p => p.PhoneNumber)
            .MustBeValueObject(PhoneNumber.Create);

        RuleFor(p => p.Weight)
            .MustBeValueObject(WeightMeasurement.CreateFromGrams);

        RuleFor(p => p.Height)
            .MustBeValueObject(HeightMeasurement.CreateFromCentimeters);
    }
}
