using CSharpFunctionalExtensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PetFamily.Core.Abstraction;
using PetFamily.Core.Database;
using PetFamily.SharedKernel.ErrorManagement;
using PetFamily.SharedKernel.Structs;
using PetFamily.Volunteers.Application.Background;
using PetFamily.Volunteers.Application.FileProvider;
using PetFamily.Volunteers.Application.FileProvider.Convertors;
using PetFamily.Volunteers.Application.Providers;
using PetFamily.Volunteers.Domain.ValueObjects;

namespace PetFamily.Volunteers.Application.Commands.UploadPhotosToPet;
public class UploadPetPhotosHandler
    : ICommandHandler<IEnumerable<string>, UploadPetPhotosCommand>
{
    private const string BUCKET_NAME = "petfamily";

    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFilesProvider _filesProvider;
    private readonly IFileCleanerQueue _fileCleanerQueue;
    //private readonly IValidator<UploadPetPhotosCommand> _validator;
    private readonly ILogger<UploadPetPhotosHandler> _logger;
    public UploadPetPhotosHandler(
        IVolunteersRepository volunteersRepository,
        [FromKeyedServices(UnitOfWorkSelector.Volunteers)] IUnitOfWork unitOfWork,
        IFilesProvider filesProvider,
        IFileCleanerQueue fileCleanerQueue,
        //IValidator<UploadPetPhotosCommand> validator,
        ILogger<UploadPetPhotosHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
        _unitOfWork = unitOfWork;
        _filesProvider = filesProvider;
        _fileCleanerQueue = fileCleanerQueue;
        //_validator = validator;
        _logger = logger;
    }

    public async Task<Result<IEnumerable<string>, ErrorList>> HandleAsync(
        UploadPetPhotosCommand command,
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

        var photosToUpload = command.Photos.Select(
                x => new FileData(x.Content, FileNameHelpers.GetRandomizedFileName(x.FileName)));

        var uploadResult = await _filesProvider.UploadFilesAsync(
            photosToUpload,
            BUCKET_NAME,
            cancellationToken);

        if (uploadResult.IsFailure)
        {
            var photosToDelete = photosToUpload.Select(p => p.ObjectName);
            await _fileCleanerQueue.PublishAsync(photosToDelete, cancellationToken);

            return uploadResult.Error;
        }

        List<Photo> photos = [];
        foreach (var uploadPhoto in uploadResult.Value)
        {
            var photo = Photo.Create(uploadPhoto);

            if (photo.IsFailure)
                return photo.Error.ToErrorList();

            photos.Add(photo.Value);
        }

        petResult.Value.AddPhotos(photos);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return uploadResult.Value.ToList();
    }
}
