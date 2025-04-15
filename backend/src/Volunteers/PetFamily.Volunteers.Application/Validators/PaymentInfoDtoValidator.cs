using FluentValidation;
using PetFamily.Core.Validation;
using PetFamily.Volunteers.Contracts.Dtos.Shared;
using PetFamily.Volunteers.Domain.ValueObjects;

namespace PetFamily.Volunteers.Application.Validators;
public class PaymentInfoDtoValidator : AbstractValidator<PaymentInfoDto>
{
    public PaymentInfoDtoValidator()
    {
        RuleFor(pi => new { pi.Address, pi.Name })
            .MustBeValueObject(pi => PaymentInfo.Create(pi.Name, pi.Address));
    }
}
