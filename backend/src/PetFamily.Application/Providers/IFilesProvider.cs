using CSharpFunctionalExtensions;
using PetFamily.Application.FileProvider;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.Providers;
public interface IFilesProvider
{
    Task<Result<string, Error>> UploadFileAsync(FileData fileData, CancellationToken cancellationToken = default);
    Task<Result<string, Error>> DeleteFileAsync(FileDataInfo fileInfo, CancellationToken cancellationToken = default);
    Task<Result<string, Error>> GetPredesignedFileLinkAsync(FileDataInfo fileInfo, CancellationToken cancellationToken = default);
}
