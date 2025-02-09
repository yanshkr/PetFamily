using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Database;
using PetFamily.Application.Extensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteers.Ids;
using PetFamily.Domain.Volunteers.ValueObjects;

namespace PetFamily.Application.Features.Volunteers.UpdateVolunteerPaymentInfo;
public class UpdateVolunteerPaymentInfoHandler
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UpdateVolunteerPaymentInfoCommand> _validator;
    private readonly ILogger<UpdateVolunteerPaymentInfoHandler> _logger;
    public UpdateVolunteerPaymentInfoHandler(
        IVolunteersRepository volunteersRepository,
        IUnitOfWork unitOfWork,
        IValidator<UpdateVolunteerPaymentInfoCommand> validator,
        ILogger<UpdateVolunteerPaymentInfoHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
        _logger = logger;
    }

    public async Task<Result<VolunteerId, ErrorList>> HandleAsync(
        UpdateVolunteerPaymentInfoCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.ToErrorList();

        var volunteer = await _volunteersRepository.GetByIdAsync(command.Id, cancellationToken);
        if (volunteer.IsFailure)
            return volunteer.Error.ToErrorList();

        var paymentInfos = command.PaymentInfos.Select(x => PaymentInfo.Create(x.Name, x.Address).Value);

        volunteer.Value.UpdatePaymentInfo(paymentInfos);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return volunteer.Value.Id;
    }
}
