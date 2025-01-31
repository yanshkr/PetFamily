using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Features.Files.UploadFile.Contracts;
using PetFamily.Application.FileProvider;
using PetFamily.Application.Providers;
using PetFamily.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Features.Files.UploadFile;
public class UploadFileHandler
{
    private readonly ILogger _logger;
    private readonly IFilesProvider _filesProvider;

    public UploadFileHandler(
        IFilesProvider filesProvider,
        ILogger<UploadFileHandler> logger
        )
    {
        _filesProvider = filesProvider;
        _logger = logger;
    }

    public async Task<Result<string, Error>> Handle(
        UploadFileCommand command,
        CancellationToken cancellationToken = default
        )
    {
        _logger.LogInformation("Uploading file {FileName}", command.FileName);

        var fileData = new FileData(command.Stream, new FileDataInfo(command.FileName));

        var result = await _filesProvider.UploadFileAsync(fileData, cancellationToken);

        _logger.LogInformation("File {FileName} uploaded", command.FileName);

        command.Stream.Dispose();

        return result;
    }
}
