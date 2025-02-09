using PetFamily.Application.Dtos;

namespace PetFamily.Application.Features.Volunteers.UpdateVolunteerPaymentInfo;
public record UpdateVolunteerPaymentInfoCommand(
    Guid Id,
    IEnumerable<PaymentInfoDto> PaymentInfos);