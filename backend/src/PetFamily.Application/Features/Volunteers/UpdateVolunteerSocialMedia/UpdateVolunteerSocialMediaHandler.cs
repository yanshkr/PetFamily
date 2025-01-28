using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Features.Volunteers.CreateVolunteer;
using PetFamily.Domain.Shared;
using PetFamily.Domain.ValueObjects;

namespace PetFamily.Application.Features.Volunteers.UpdateVolunteerSocialMedia;
public class UpdateVolunteerSocialMediaHandler
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly ILogger<CreateVolunteerHandler> _logger;
    public UpdateVolunteerSocialMediaHandler(
        IVolunteersRepository volunteersRepository,
        ILogger<CreateVolunteerHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
        _logger = logger;
    }

    public async Task<Result<Guid, Error>> HandleAsync(
        UpdateVolunteerSocialMediaRequest request,
        CancellationToken cancellationToken = default
        )
    {
        _logger.LogDebug("UpdateVolunteerSocialMediaRequest: {@request}", request);

        var volunteer = await _volunteersRepository.GetByIdAsync(request.Id, cancellationToken);
        if (volunteer.IsFailure)
            return volunteer.Error;

        var socialMedias = request.Dto.SocialMedias.Select(x => SocialMedia.Create(x.Type, x.Url).Value);

        volunteer.Value.UpdateSocialMedia(socialMedias);

        await _volunteersRepository.SaveAsync(cancellationToken);

        _logger.LogDebug("Volunteer social media updated: {@id}", volunteer.Value.Id.Value);

        return volunteer.Value.Id.Value;
    }
}
