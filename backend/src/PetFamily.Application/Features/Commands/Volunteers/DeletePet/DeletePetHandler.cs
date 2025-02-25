using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Abstraction;
using PetFamily.Application.Background;
using PetFamily.Application.Database;
using PetFamily.Application.Providers;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteers.Ids;

namespace PetFamily.Application.Features.Commands.Volunteers.DeletePet;
public class DeletePetHandler
    : ICommandHandler<PetId, DeletePetCommand>
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IFileCleanerQueue _fileCleanerQueue;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFilesProvider _filesProvider;
    private readonly ILogger<DeletePetHandler> _logger;
    public DeletePetHandler(
        IVolunteersRepository volunteersRepository,
        IFileCleanerQueue fileCleanerQueue,
        IUnitOfWork unitOfWork,
        IFilesProvider filesProvider,
        ILogger<DeletePetHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
        _fileCleanerQueue = fileCleanerQueue;
        _unitOfWork = unitOfWork;
        _filesProvider = filesProvider;
        _logger = logger;
    }

    public async Task<Result<PetId, ErrorList>> HandleAsync(
        DeletePetCommand command,
        CancellationToken cancellationToken = default)
    {
        //var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        //if (!validationResult.IsValid)
        //    return validationResult.ToErrorList();

        var volunteerResult = await _volunteersRepository.GetByIdAsync(command.VolunteerId, cancellationToken);
        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();

        var petResult = volunteerResult.Value.GetPetById(command.PetId);
        if (petResult.IsFailure)
            return petResult.Error.ToErrorList();

        if (volunteerResult.Value.IsDeleted && command.IsSoftDelete)
            return petResult.Value.Id;

        if (command.IsSoftDelete)
        {
            petResult.Value.Delete();
        }
        else
        {
            volunteerResult.Value.RemovePet(petResult.Value);

            await _fileCleanerQueue.PublishAsync(petResult.Value.Photos.Select(x => x.FileName), cancellationToken);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return petResult.Value.Id;
    }
}
