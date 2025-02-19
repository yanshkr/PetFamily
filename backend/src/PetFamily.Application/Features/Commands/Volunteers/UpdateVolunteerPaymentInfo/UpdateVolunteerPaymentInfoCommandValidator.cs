using FluentValidation;
using PetFamily.Application.Dtos.Validators;

namespace PetFamily.Application.Features.Commands.Volunteers.UpdateVolunteerPaymentInfo;
public class UpdateVolunteerPaymentInfoCommandValidator : AbstractValidator<UpdateVolunteerPaymentInfoCommand>
{
    public UpdateVolunteerPaymentInfoCommandValidator()
    {
        RuleForEach(up => up.PaymentInfos)
            .SetValidator(new PaymentInfoDtoValidator());
    }
}
