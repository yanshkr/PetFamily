using FluentValidation;
using PetFamily.Application.Validation;
using PetFamily.Domain.Volunteers.ValueObjects;

namespace PetFamily.Application.Dtos.Validators;
public class PaymentInfoDtoValidator : AbstractValidator<PaymentInfoDto>
{
    public PaymentInfoDtoValidator()
    {
        RuleFor(pi => new { pi.Address, pi.Name })
            .MustBeValueObject(pi => PaymentInfo.Create(pi.Name, pi.Address));
    }
}
