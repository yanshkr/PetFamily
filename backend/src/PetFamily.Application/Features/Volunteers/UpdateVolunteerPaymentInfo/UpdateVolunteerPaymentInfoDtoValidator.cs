using FluentValidation;
using PetFamily.Application.Features.Volunteers.UpdateVolunteerPaymentInfo.Contracts;
using PetFamily.Application.Validation;
using PetFamily.Domain.ValueObjects;

namespace PetFamily.Application.Features.Volunteers.UpdateVolunteerPaymentInfo;
public class UpdateVolunteerPaymentInfoDtoValidator : AbstractValidator<UpdateVolunteerPaymentInfoDto>
{
    public UpdateVolunteerPaymentInfoDtoValidator()
    {
        RuleForEach(x => x.PaymentInfos)
            .MustBeValueObject(s => PaymentInfo.Create(s.Name, s.Address));
    }
}
