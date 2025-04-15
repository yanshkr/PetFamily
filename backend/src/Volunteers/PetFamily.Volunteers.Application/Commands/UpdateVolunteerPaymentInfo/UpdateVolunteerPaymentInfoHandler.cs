using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PetFamily.Core.Abstraction;
using PetFamily.Core.Database;
using PetFamily.Core.Extensions;
using PetFamily.SharedKernel.ErrorManagement;
using PetFamily.SharedKernel.Structs;
using PetFamily.Volunteers.Domain.Ids;
using PetFamily.Volunteers.Domain.ValueObjects;

namespace PetFamily.Volunteers.Application.Commands.UpdateVolunteerPaymentInfo;
public class UpdateVolunteerPaymentInfoHandler
    : ICommandHandler<VolunteerId, UpdateVolunteerPaymentInfoCommand>
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UpdateVolunteerPaymentInfoCommand> _validator;
    private readonly ILogger<UpdateVolunteerPaymentInfoHandler> _logger;
    public UpdateVolunteerPaymentInfoHandler(
        IVolunteersRepository volunteersRepository,
        [FromKeyedServices(UnitOfWorkSelector.Volunteers)] IUnitOfWork unitOfWork,
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

        var volunteerResult = await _volunteersRepository.GetByIdAsync(command.Id, cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();

        var paymentInfos = command.PaymentInfos.Select(x => PaymentInfo.Create(x.Name, x.Address).Value);

        volunteerResult.Value.UpdatePaymentInfo(paymentInfos);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return volunteerResult.Value.Id;
    }
}
