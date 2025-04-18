﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PetFamily.Volunteers.Application.Background;
using PetFamily.Volunteers.Application.Providers;

namespace PetFamily.Volunteers.Infrastructure.BackgroudServices;
public class RedudantFileCleanerService : BackgroundService
{
    private const string BUCKET_NAME = "petfamily";

    private readonly ILogger<RedudantFileCleanerService> _logger;
    private readonly IFileCleanerQueue _fileCleanerQueue;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    public RedudantFileCleanerService(
        ILogger<RedudantFileCleanerService> logger,
        IFileCleanerQueue fileCleanerQueue,
        IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;
        _fileCleanerQueue = fileCleanerQueue;
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("RedudantFileCleanerService is starting.");

        while (!stoppingToken.IsCancellationRequested)
        {
            await using var scope = _serviceScopeFactory.CreateAsyncScope();
            var filesProvider = scope.ServiceProvider.GetRequiredService<IFilesProvider>();

            var filesToDelete = await _fileCleanerQueue.ConsumeAsync(stoppingToken);

            _logger.LogInformation("Deleting {0} files", filesToDelete.Count());
            await filesProvider.DeleteFilesAsync(filesToDelete, BUCKET_NAME, stoppingToken);
        }

        _logger.LogInformation("RedudantFileCleanerService is stopping.");
    }
}
