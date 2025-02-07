using PetFamily.Application.Dtos;
using PetFamily.Application.Features.Volunteers.UpdateVolunteerPaymentInfo;
using PetFamily.Domain.Volunteers.Ids;

namespace PetFamily.Api.Controllers.Volunteers.Requests;
public record UpdateVolunteerPaymentInfoRequest(IEnumerable<PaymentInfoDto> PaymentInfos)
{
    public UpdateVolunteerPaymentInfoCommand ToCommand(VolunteerId Id)
        => new(Id, PaymentInfos);
}
