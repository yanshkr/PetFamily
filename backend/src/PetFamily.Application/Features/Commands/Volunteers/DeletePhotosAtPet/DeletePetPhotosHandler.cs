using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Abstraction;
using PetFamily.Application.Database;
using PetFamily.Application.Providers;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteers.ValueObjects;

namespace PetFamily.Application.Features.Commands.Volunteers.DeletePhotosAtPet;
public class DeletePetPhotosHandler
    : ICommandHandler<IEnumerable<string>, DeletePetPhotosCommand>
{
    private const string BUCKET_NAME = "petfamily";

    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFilesProvider _filesProvider;
    //private readonly IValidator<DeletePetPhotosCommand> _validator;
    private readonly ILogger<DeletePetPhotosHandler> _logger;
    public DeletePetPhotosHandler(
        IVolunteersRepository volunteersRepository,
        IUnitOfWork unitOfWork,
        IFilesProvider filesProvider,
        //IValidator<DeletePetPhotosCommand> validator,
        ILogger<DeletePetPhotosHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
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

        var volunteer = await _volunteersRepository.GetByIdAsync(command.VolunteerId, cancellationToken);
        if (volunteer.IsFailure)
            return volunteer.Error.ToErrorList();

        var pet = volunteer.Value.GetPetById(command.PetId);
        if (pet.IsFailure)
            return pet.Error.ToErrorList();

        var deleteResults = await _filesProvider.DeleteFilesAsync(command.Photos, BUCKET_NAME, cancellationToken);

        if (deleteResults.IsFailure)
            return deleteResults.Error;

        List<Photo> photos = [];
        foreach (var deletePhoto in command.Photos)
        {
            var photo = Photo.Create(deletePhoto);

            if (photo.IsFailure)
                return photo.Error.ToErrorList();

            photos.Add(photo.Value);
        }

        pet.Value.RemovePhotos(photos);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return command.Photos.ToList();
    }
}
