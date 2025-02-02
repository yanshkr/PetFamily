namespace PetFamily.Infrastructure.Constants;
public static class MinioConstants
{
    public const string OPTIONS_NAME = "Minio";
    public const string BUCKET_NAME = "petfamily";

    public static readonly TimeSpan DEFAULT_EXPIRY = TimeSpan.FromDays(1);
}
