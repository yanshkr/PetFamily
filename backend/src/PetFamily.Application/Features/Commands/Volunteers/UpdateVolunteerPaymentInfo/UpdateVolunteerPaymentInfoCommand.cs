using PetFamily.Application.Abstraction;
using PetFamily.Application.Dtos;

namespace PetFamily.Application.Features.Commands.Volunteers.UpdateVolunteerPaymentInfo;
public record UpdateVolunteerPaymentInfoCommand(
    Guid Id,
    IEnumerable<PaymentInfoDto> PaymentInfos) : ICommand;