using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Database;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Species.Ids;

namespace PetFamily.Application.Features.Commands.Species.Delete;
public class DeleteSpecieHandler
{
    private readonly ISpeciesRepository _speciesRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteSpecieHandler> _logger;
    public DeleteSpecieHandler(
        ISpeciesRepository speciesRepository,
        IUnitOfWork unitOfWork,
        ILogger<DeleteSpecieHandler> logger)
    {
        _speciesRepository = speciesRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<PetSpecieId, ErrorList>> HandleAsync(
        DeleteSpecieCommand command,
        CancellationToken cancellationToken = default)
    {
        var specieResult = await _speciesRepository.GetByIdAsync(command.Id, cancellationToken);

        if (specieResult.IsFailure)
            return specieResult.Error.ToErrorList();

        _speciesRepository.Delete(specieResult.Value);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return specieResult.Value.Id;
    }
}