using PetFamily.Domain.Volunteers.Ids;

namespace PetFamily.Application.Features.Volunteers.UpdateVolunteerPaymentInfo;
public record UpdateVolunteerPaymentInfoRequest(
    VolunteerId Id,
    UpdateVolunteerPaymentInfoDto Dto
    );

public record UpdateVolunteerPaymentInfoDto(
    IEnumerable<PaymentInfoRaw> PaymentInfos
    );

public record PaymentInfoRaw(
    string Name,
    string Address
    );
