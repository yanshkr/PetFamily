using CSharpFunctionalExtensions;
using Minio;
using Minio.DataModel.Args;
using PetFamily.Application.FileProvider;
using PetFamily.Application.Providers;
using PetFamily.Domain.Shared;
using PetFamily.Infrastructure.Constants;
using System.Collections.Concurrent;

namespace PetFamily.Infrastructure.Providers;
public class MinioProvider : IFilesProvider
{
    private readonly IMinioClient _minioClient;
    public MinioProvider(IMinioClient minioClient)
    {
        _minioClient = minioClient;
    }
    public async Task<Result<IEnumerable<string>, ErrorList>> UploadFilesAsync(
        IEnumerable<FileData> filesData,
        string bucketName,
        CancellationToken cancellationToken = default)
    {
        var ensureMakeBucketResult = await EnsureMakeBucketAsync(bucketName, cancellationToken);

        if (ensureMakeBucketResult.IsFailure)
            return ensureMakeBucketResult.Error.ToErrorList();

        var options = new ParallelOptions
        {
            MaxDegreeOfParallelism = MinioConstants.MAX_CONCURRENCY
        };

        ConcurrentBag<string> uploadFiles = [];
        List<Error> uploadErrors = [];
        await Parallel.ForEachAsync(filesData, options, async (fileData, cancellationToken) =>
        {
            var putObjectArgs = new PutObjectArgs()
                .WithBucket(bucketName)
                .WithStreamData(fileData.Stream)
                .WithObjectSize(fileData.Stream.Length)
                .WithObject(fileData.ObjectName);

            try
            {
                var uploadResult = await _minioClient.PutObjectAsync(putObjectArgs, cancellationToken);

                uploadFiles.Add(uploadResult.ObjectName);
            }
            catch
            {
                uploadErrors.Add(Errors.General.UploadFailure(
                    $"Failed to upload file '{fileData.ObjectName}' to bucket '{bucketName}'"));
            }
        });

        if (uploadErrors.Count != 0)
            return new ErrorList(uploadErrors);

        return uploadFiles;
    }
    public async Task<UnitResult<ErrorList>> DeleteFilesAsync(
        IEnumerable<string> objectsName,
        string bucketName,
        CancellationToken cancellationToken = default)
    {
        List<Error> deleteErrors = [];
        foreach (var objectName in objectsName)
        {
            var fileExistsResult = await IsFileExistsAsync(objectName, bucketName, cancellationToken);

            if (fileExistsResult.IsFailure)
            {
                deleteErrors.Add(fileExistsResult.Error);
                continue;
            }
            else if (!fileExistsResult.Value)
            {
                continue;
            }

            var removeObjectArgs = new RemoveObjectArgs()
                .WithBucket(bucketName)
                .WithObject(objectName);

            try
            {
                await _minioClient.RemoveObjectAsync(removeObjectArgs, cancellationToken);
            }
            catch
            {
                deleteErrors.Add(Errors.General.UploadFailure(
                    $"Failed to delete file '{objectName}' from bucket '{bucketName}'."));
            }
        }

        return UnitResult.Success<ErrorList>();
    }
    //public async Task<Result<string, Error>> GetPredesignedFileLinkAsync(
    //    string objectName,
    //    string bucketName,
    //    CancellationToken cancellationToken = default)
    //{
    //    var presignedGetObjectArgs = new PresignedGetObjectArgs()
    //        .WithBucket(bucketName)
    //        .WithObject(objectName)
    //        .WithExpiry(MinioConstants.DEFAULT_EXPIRY);

    //    try
    //    {
    //        var presignedUrl = await _minioClient.PresignedGetObjectAsync(presignedGetObjectArgs);

    //        return presignedUrl;
    //    }
    //    catch
    //    {
    //        return Errors.General.UploadFailure(
    //            $"Failed to get presigned link for file '{objectName}' from bucket '{bucketName}'.");
    //    }
    //}
    private async Task<Result<bool, Error>> IsFileExistsAsync(
        string objectName,
        string bucketName,
        CancellationToken cancellationToken = default)
    {
        var bucketExistsResult = await IsBucketExistsAsync(bucketName, cancellationToken);

        if (bucketExistsResult.IsFailure)
            return bucketExistsResult;

        if (!bucketExistsResult.Value)
            return false;

        var objectExistsArgs = new StatObjectArgs()
            .WithBucket(bucketName)
            .WithObject(objectName);

        try
        {
            var objectStatInfo = await _minioClient.StatObjectAsync(objectExistsArgs, cancellationToken);

            return objectStatInfo.ObjectName != string.Empty;
        }
        catch
        {
            return Errors.General.UploadFailure(
                $"Failed to check if object '{objectName}' exists in bucket '{bucketName}'");
        }
    }
    private async Task<Result<bool, Error>> IsBucketExistsAsync(
        string bucketName,
        CancellationToken cancellationToken = default)
    {
        var bucketExistsArgs = new BucketExistsArgs()
            .WithBucket(bucketName);

        try
        {
            return await _minioClient.BucketExistsAsync(bucketExistsArgs, cancellationToken);
        }
        catch
        {
            return Errors.General.UploadFailure($"Failed to check if bucket '{bucketName}' exists");
        }
    }
    private async Task<UnitResult<Error>> EnsureMakeBucketAsync(
        string bucketName,
        CancellationToken cancellationToken = default)
    {
        var bucketExistsResult = await IsBucketExistsAsync(bucketName, cancellationToken);

        if (bucketExistsResult.IsFailure)
            return bucketExistsResult;

        if (bucketExistsResult.Value)
            return UnitResult.Success<Error>();

        var makeBucketArgs = new MakeBucketArgs()
            .WithBucket(bucketName);

        try
        {
            await _minioClient.MakeBucketAsync(makeBucketArgs, cancellationToken);
        }
        catch
        {
            return Errors.General.UploadFailure($"Failed to create bucket '{bucketName}'");
        }

        return UnitResult.Success<Error>();
    }
}
