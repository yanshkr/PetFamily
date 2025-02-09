using PetFamily.Application.Dtos;
using PetFamily.Application.Features.Volunteers.UpdateVolunteerPaymentInfo;

namespace PetFamily.Api.Controllers.Volunteers.Requests;
public record UpdateVolunteerPaymentInfoRequest(IEnumerable<PaymentInfoDto> PaymentInfos)
{
    public UpdateVolunteerPaymentInfoCommand ToCommand(Guid Id)
        => new(Id, PaymentInfos);
}
