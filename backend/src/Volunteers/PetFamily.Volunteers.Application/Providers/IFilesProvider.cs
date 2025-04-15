using CSharpFunctionalExtensions;
using PetFamily.SharedKernel.ErrorManagement;
using PetFamily.Volunteers.Application.FileProvider;

namespace PetFamily.Volunteers.Application.Providers;
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
