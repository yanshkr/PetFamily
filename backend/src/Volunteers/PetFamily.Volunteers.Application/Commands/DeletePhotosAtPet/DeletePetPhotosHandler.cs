using CSharpFunctionalExtensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PetFamily.Core.Abstraction;
using PetFamily.Core.Database;
using PetFamily.SharedKernel.ErrorManagement;
using PetFamily.SharedKernel.Structs;
using PetFamily.Volunteers.Application.Background;
using PetFamily.Volunteers.Application.Providers;
using PetFamily.Volunteers.Domain.ValueObjects;

namespace PetFamily.Volunteers.Application.Commands.DeletePhotosAtPet;
public class DeletePetPhotosHandler
    : ICommandHandler<IEnumerable<string>, DeletePetPhotosCommand>
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IFileCleanerQueue _fileCleanerQueue;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFilesProvider _filesProvider;
    //private readonly IValidator<DeletePetPhotosCommand> _validator;
    private readonly ILogger<DeletePetPhotosHandler> _logger;
    public DeletePetPhotosHandler(
        IVolunteersRepository volunteersRepository,
        IFileCleanerQueue fileCleanerQueue,
        [FromKeyedServices(UnitOfWorkSelector.Volunteers)] IUnitOfWork unitOfWork,
        IFilesProvider filesProvider,
        //IValidator<DeletePetPhotosCommand> validator,
        ILogger<DeletePetPhotosHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
        _fileCleanerQueue = fileCleanerQueue;
        _unitOfWork = unitOfWork;
        _filesProvider = filesProvider;
        //_validator = validator;
        _logger = logger;
    }

    public async Task<Result<IEnumerable<string>, ErrorList>> HandleAsync(
        DeletePetPhotosCommand command,
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

        List<Photo> photos = [];
        foreach (var deletePhoto in command.Photos)
        {
            var photo = Photo.Create(deletePhoto);

            if (photo.IsFailure)
                return photo.Error.ToErrorList();

            photos.Add(photo.Value);
        }

        petResult.Value.RemovePhotos(photos);

        await _fileCleanerQueue.PublishAsync(photos.Select(x => x.FileName), cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return command.Photos.ToList();
    }
}
