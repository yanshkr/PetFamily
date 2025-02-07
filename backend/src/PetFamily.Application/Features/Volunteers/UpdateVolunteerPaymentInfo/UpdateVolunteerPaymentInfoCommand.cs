using PetFamily.Application.Dtos;
using PetFamily.Domain.Volunteers.Ids;

namespace PetFamily.Application.Features.Volunteers.UpdateVolunteerPaymentInfo;
public record UpdateVolunteerPaymentInfoCommand(
    VolunteerId Id,
    IEnumerable<PaymentInfoDto> PaymentInfos);