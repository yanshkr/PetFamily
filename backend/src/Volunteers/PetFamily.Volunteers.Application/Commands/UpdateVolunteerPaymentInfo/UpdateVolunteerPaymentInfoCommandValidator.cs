using FluentValidation;
using PetFamily.Volunteers.Application.Validators;

namespace PetFamily.Volunteers.Application.Commands.UpdateVolunteerPaymentInfo;
public class UpdateVolunteerPaymentInfoCommandValidator : AbstractValidator<UpdateVolunteerPaymentInfoCommand>
{
    public UpdateVolunteerPaymentInfoCommandValidator()
    {
        RuleForEach(up => up.PaymentInfos)
            .SetValidator(new PaymentInfoDtoValidator());
    }
}
