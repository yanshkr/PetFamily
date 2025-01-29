using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Domain.Shared;
using PetFamily.Domain.ValueObjects;

namespace PetFamily.Application.Features.Volunteers.UpdateVolunteerPaymentInfo;
public class UpdateVolunteerPaymentInfoHandler
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly ILogger<UpdateVolunteerPaymentInfoHandler> _logger;
    public UpdateVolunteerPaymentInfoHandler(
        IVolunteersRepository volunteersRepository,
        ILogger<UpdateVolunteerPaymentInfoHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
        _logger = logger;
    }

    public async Task<Result<Guid, Error>> HandleAsync(
        UpdateVolunteerPaymentInfoRequest request,
        CancellationToken cancellationToken = default
        )
    {
        _logger.LogDebug("UpdateVolunteerPaymentInfoRequest: {@request}", request);

        var volunteer = await _volunteersRepository.GetByIdAsync(request.Id, cancellationToken);
        if (volunteer.IsFailure)
            return volunteer.Error;

        var paymentInfos = request.Dto.PaymentInfos.Select(x => PaymentInfo.Create(x.Name, x.Address).Value);

        volunteer.Value.UpdatePaymentInfo(paymentInfos);

        await _volunteersRepository.SaveAsync(cancellationToken);

        _logger.LogDebug("Volunteer payment info updated: {@id}", volunteer.Value.Id.Value);

        return volunteer.Value.Id.Value;
    }
}
