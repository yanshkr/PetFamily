using PetFamily.Volunteers.Application.Commands.UpdateVolunteerPaymentInfo;
using PetFamily.Volunteers.Contracts.Dtos.Shared;

namespace PetFamily.Volunteers.Presentation.Volunteers.Requests;
public record UpdateVolunteerPaymentInfoRequest(IEnumerable<PaymentInfoDto> PaymentInfos)
{
    public UpdateVolunteerPaymentInfoCommand ToCommand(Guid Id)
        => new(Id, PaymentInfos);
}
