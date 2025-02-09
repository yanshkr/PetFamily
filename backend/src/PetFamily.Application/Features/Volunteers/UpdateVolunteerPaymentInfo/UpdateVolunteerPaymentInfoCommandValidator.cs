using FluentValidation;
using PetFamily.Application.Dtos.Validators;

namespace PetFamily.Application.Features.Volunteers.UpdateVolunteerPaymentInfo;
public class UpdateVolunteerPaymentInfoCommandValidator : AbstractValidator<UpdateVolunteerPaymentInfoCommand>
{
    public UpdateVolunteerPaymentInfoCommandValidator()
    {
        RuleForEach(up => up.PaymentInfos)
            .SetValidator(new PaymentInfoDtoValidator());
    }
}
