using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Database;
using PetFamily.Application.Features.Volunteers.UploadPhotoToPet;
using PetFamily.Application.FileProvider;
using PetFamily.Application.FileProvider.Convertors;
using PetFamily.Application.Providers;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteers.ValueObjects;

namespace PetFamily.Application.Features.Volunteers.UploadPhotosToPet;
public class UploadPetPhotosHandler
{
    private const string BUCKET_NAME = "petfamily";

    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFilesProvider _filesProvider;
    //private readonly IValidator<UploadPetPhotosCommand> _validator;
    private readonly ILogger<UploadPetPhotosHandler> _logger;
    public UploadPetPhotosHandler(
        IVolunteersRepository volunteersRepository,
        IUnitOfWork unitOfWork,
        IFilesProvider filesProvider,
        //IValidator<UploadPetPhotosCommand> validator,
        ILogger<UploadPetPhotosHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
        _unitOfWork = unitOfWork;
        _filesProvider = filesProvider;
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

        var volunteer = await _volunteersRepository.GetByIdAsync(command.VolunteerId, cancellationToken);
        if (volunteer.IsFailure)
            return volunteer.Error.ToErrorList();

        var pet = volunteer.Value.GetPetById(command.PetId);
        if (pet.IsFailure)
            return pet.Error.ToErrorList();

        var uploadResult = await _filesProvider.UploadFilesAsync(
            command.Photos.Select(
                x => new FileData(x.Content, FileNameHelpers.GetRandomizedFileName(x.FileName))),
            BUCKET_NAME,
            cancellationToken);

        if (uploadResult.IsFailure)
            return uploadResult.Error;

        List<Photo> photos = [];
        foreach (var uploadPhoto in uploadResult.Value)
        {
            var photo = Photo.Create(uploadPhoto);

            if (photo.IsFailure)
                return photo.Error.ToErrorList();

            photos.Add(photo.Value);
        }

        pet.Value.AddPhotos(photos);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return uploadResult.Value.ToList();
    }
}
