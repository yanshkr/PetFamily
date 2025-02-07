using CSharpFunctionalExtensions;
using PetFamily.Application.FileProvider;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.Providers;
public interface IFilesProvider
{
    Task<Result<IEnumerable<string>, ErrorList>> UploadFilesAsync(
        IEnumerable<FileData> filesData,
        string bucketName,
        CancellationToken cancellationToken = default);
    Task<UnitResult<ErrorList>> DeleteFilesAsync(
        IEnumerable<string> objectsName,
        string bucketName,
        CancellationToken cancellationToken = default);
    //Task<Result<string, Error>> GetPredesignedFileLinkAsync(
    //    string objectName,
    //    string bucketName,
    //    CancellationToken cancellationToken = default);
}
