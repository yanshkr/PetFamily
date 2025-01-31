using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Features.Files.GetFileUri.Contracts;
using PetFamily.Application.FileProvider;
using PetFamily.Application.Providers;
using PetFamily.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Features.Files.GetFileUri;
public class GetFileUriHandler
{
    private readonly ILogger _logger;
    private readonly IFilesProvider _filesProvider;

    public GetFileUriHandler(
        IFilesProvider filesProvider,
        ILogger<GetFileUriHandler> logger
        )
    {
        _filesProvider = filesProvider;
        _logger = logger;
    }

    public async Task<Result<string, Error>> Handle(
        GetFileUriCommand command,
        CancellationToken cancellationToken = default
        )
    {
        _logger.LogInformation("Getting file {FileName} uri", command.FileName);

        var fileDataInfo = new FileDataInfo(command.FileName);

        return await _filesProvider.GetPredesignedFileLinkAsync(fileDataInfo, cancellationToken);
    }
}