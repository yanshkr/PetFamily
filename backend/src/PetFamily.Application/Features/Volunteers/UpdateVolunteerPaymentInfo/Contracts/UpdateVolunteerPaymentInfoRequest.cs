using PetFamily.Domain.Volunteers.Ids;

namespace PetFamily.Application.Features.Volunteers.UpdateVolunteerPaymentInfo.Contracts;
public record UpdateVolunteerPaymentInfoRequest(
    VolunteerId Id,
    UpdateVolunteerPaymentInfoDto Dto);