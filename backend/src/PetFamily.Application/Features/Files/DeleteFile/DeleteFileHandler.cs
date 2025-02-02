using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Features.Files.DeleteFile.Contracts;
using PetFamily.Application.FileProvider;
using PetFamily.Application.Providers;
using PetFamily.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Features.Files.DeleteFile;
public class DeleteFileHandler
{
    private readonly ILogger _logger;
    private readonly IFilesProvider _filesProvider;

    public DeleteFileHandler(
        IFilesProvider filesProvider,
        ILogger<DeleteFileHandler> logger)
    {
        _filesProvider = filesProvider;
        _logger = logger;
    }

    public async Task<Result<string, Error>> Handle(
        DeleteFileCommand command,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Deleting file {FileName}", command.FileName);

        var fileDataInfo = new FileDataInfo(command.FileName);

        return await _filesProvider.DeleteFileAsync(fileDataInfo, cancellationToken);
    }
}