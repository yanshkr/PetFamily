using CSharpFunctionalExtensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NSubstitute;
using PetFamily.Application.FileProvider;
using PetFamily.Application.Providers;
using PetFamily.Domain.Shared;

namespace PetFamily.Application.IntegrationTests.Volunteers;
public class VolunteersTestsWebFactory : IntegrationTestsWebFactory
{
    private readonly IFilesProvider _fileProviderMock;
    public VolunteersTestsWebFactory()
    {
        _fileProviderMock = Substitute.For<IFilesProvider>();
    }
    protected override void ConfigureDefaultServices(IServiceCollection services)
    {
        base.ConfigureDefaultServices(services);

        services.RemoveAll(typeof(IFilesProvider));

        services.AddScoped(_ => _fileProviderMock);
    }

    public void SetupFileProviderSuccessUploadMock()
    {
        _fileProviderMock
            .UploadFilesAsync(Arg.Any<IEnumerable<FileData>>(), Arg.Any<string>())
            .Returns(Result.Success<IEnumerable<string>, ErrorList>(["photo1.png", "photo2.png"]));
    }
    public void SetupFileProviderFailedUploadMock()
    {
        var errorList = new ErrorList([Errors.General.UploadFailure("test")]);

        _fileProviderMock
            .UploadFilesAsync(Arg.Any<IEnumerable<FileData>>(), Arg.Any<string>())
            .Returns(Result.Failure<IEnumerable<string>, ErrorList>(errorList));
    }
}
