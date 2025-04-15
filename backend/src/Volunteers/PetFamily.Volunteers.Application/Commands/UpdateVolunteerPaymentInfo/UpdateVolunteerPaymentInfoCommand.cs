using PetFamily.Core.Abstraction;
using PetFamily.Volunteers.Contracts.Dtos.Shared;

namespace PetFamily.Volunteers.Application.Commands.UpdateVolunteerPaymentInfo;
public record UpdateVolunteerPaymentInfoCommand(
    Guid Id,
    IEnumerable<PaymentInfoDto> PaymentInfos) : ICommand;