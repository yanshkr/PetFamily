using CSharpFunctionalExtensions;
using Minio;
using Minio.DataModel.Args;
using Minio.DataModel.Response;
using PetFamily.Application.FileProvider;
using PetFamily.Application.Providers;
using PetFamily.Domain.Shared;
using PetFamily.Infrastructure.Constants;
using System.IO;

namespace PetFamily.Infrastructure.Providers;
public class MinioProvider : IFilesProvider
{
    private readonly IMinioClient _minioClient;
    public MinioProvider(IMinioClient minioClient)
    {
        _minioClient = minioClient;
    }
    public async Task<Result<string, Error>> UploadFileAsync(
        FileData fileData,
        CancellationToken cancellationToken = default
        )
    {
        var ensureMakeBucketResult = await EnsureMakeBucketAsync(MinioConstants.BUCKET_NAME, cancellationToken);

        if (ensureMakeBucketResult.IsFailure)
            return ensureMakeBucketResult.ConvertFailure<string>();

        var putObjectArgs = new PutObjectArgs()
            .WithBucket(MinioConstants.BUCKET_NAME)
            .WithStreamData(fileData.Stream)
            .WithObjectSize(fileData.Stream.Length)
            .WithObject(Guid.NewGuid().ToString() + '_' + fileData.FileInfo.ObjectName);

        try
        {
            var uploadResult = await _minioClient.PutObjectAsync(putObjectArgs, cancellationToken);

            return uploadResult.ObjectName;
        }
        catch
        {
            return Errors.General.UploadFailure($"Failed to upload file '{fileData.FileInfo.ObjectName}' to bucket '{MinioConstants.BUCKET_NAME}'");
        }
    }
    public async Task<Result<string, Error>> DeleteFileAsync(
        FileDataInfo fileInfo, 
        CancellationToken cancellationToken = default
        )
    {
        var objectExistsResult = await IsFileExists(MinioConstants.BUCKET_NAME, fileInfo.ObjectName, cancellationToken);

        if (objectExistsResult.IsFailure)
            return objectExistsResult.Error;

        if (!objectExistsResult.Value)
            return fileInfo.ObjectName;

        var removeObjectArgs = new RemoveObjectArgs()
            .WithBucket(MinioConstants.BUCKET_NAME)
            .WithObject(fileInfo.ObjectName);

        try
        {
            await _minioClient.RemoveObjectAsync(removeObjectArgs, cancellationToken);
        }
        catch
        {
            return Errors.General.UploadFailure($"Failed to delete file '{fileInfo.ObjectName}' from bucket '{MinioConstants.BUCKET_NAME}'");
        }

        return fileInfo.ObjectName;
    }

    public async Task<Result<string, Error>> GetPredesignedFileLinkAsync(
        FileDataInfo fileInfo, 
        CancellationToken cancellationToken = default
        )
    {
        var presignedGetObjectArgs = new PresignedGetObjectArgs()
            .WithBucket(MinioConstants.BUCKET_NAME)
            .WithObject(fileInfo.ObjectName)
            .WithExpiry((int)MinioConstants.DEFAULT_EXPIRY.TotalSeconds);

        try
        {
            var presignedUrl = await _minioClient.PresignedGetObjectAsync(presignedGetObjectArgs);

            return presignedUrl;
        }
        catch
        {
            return Errors.General.UploadFailure($"Failed to get presigned link for file '{fileInfo.ObjectName}' from bucket '{MinioConstants.BUCKET_NAME}'");
        }
    }
    private async Task<Result<bool, Error>> IsFileExists(
        string bucketName,
        string objectName,
        CancellationToken cancellationToken = default
        )
    {
        var bucketExistsResult = await IsBucketExists(bucketName, cancellationToken);

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
            return Errors.General.UploadFailure($"Failed to check if object '{objectName}' exists in bucket '{bucketName}'");
        }
    }
    private async Task<Result<bool, Error>> IsBucketExists(
        string bucketName,
        CancellationToken cancellationToken = default
        )
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
        var bucketExistsResult = await IsBucketExists(bucketName, cancellationToken);

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
