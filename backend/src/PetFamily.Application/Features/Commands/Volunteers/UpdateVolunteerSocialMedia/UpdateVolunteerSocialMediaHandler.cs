using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Abstraction;
using PetFamily.Application.Database;
using PetFamily.Application.Extensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteers.Ids;
using PetFamily.Domain.Volunteers.ValueObjects;

namespace PetFamily.Application.Features.Commands.Volunteers.UpdateVolunteerSocialMedia;
public class UpdateVolunteerSocialMediaHandler
    : ICommandHandler<VolunteerId, UpdateVolunteerSocialMediaCommand>
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UpdateVolunteerSocialMediaCommand> _validator;
    private readonly ILogger<UpdateVolunteerSocialMediaHandler> _logger;
    public UpdateVolunteerSocialMediaHandler(
        IVolunteersRepository volunteersRepository,
        IUnitOfWork unitOfWork,
        IValidator<UpdateVolunteerSocialMediaCommand> validator,
        ILogger<UpdateVolunteerSocialMediaHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
        _logger = logger;
    }

    public async Task<Result<VolunteerId, ErrorList>> HandleAsync(
        UpdateVolunteerSocialMediaCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.ToErrorList();

        var volunteerResult = await _volunteersRepository.GetByIdAsync(command.Id, cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();

        var socialMedias = command.SocialMedias.Select(x => SocialMedia.Create(x.Type, x.Url).Value);

        volunteerResult.Value.UpdateSocialMedia(socialMedias);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return volunteerResult.Value.Id;
    }
}
