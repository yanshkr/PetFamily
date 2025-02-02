namespace PetFamily.Application.Features.Volunteers.UpdateVolunteerPaymentInfo.Contracts;

public record UpdateVolunteerPaymentInfoDto(IEnumerable<PaymentInfoRaw> PaymentInfos);
