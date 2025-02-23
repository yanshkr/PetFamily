using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Abstraction;
using PetFamily.Application.Database;
using PetFamily.Application.Features.Commands.Species.Create;
using PetFamily.Application.Features.Commands.Species.Delete;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Species.Ids;

namespace PetFamily.Application.Features.Commands.Species.DeleteBreed;
public class DeleteBreedHandler
    : ICommandHandler<PetBreedId, DeleteBreedCommand>
{
    private readonly ISpeciesRepository _speciesRepository;
    private readonly IReadDbContext _readDbContext;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteSpecieHandler> _logger;
    public DeleteBreedHandler(
        ISpeciesRepository speciesRepository,
        IReadDbContext readDbContext,
        IUnitOfWork unitOfWork,
        ILogger<DeleteSpecieHandler> logger)
    {
        _speciesRepository = speciesRepository;
        _readDbContext = readDbContext;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<PetBreedId, ErrorList>> HandleAsync(
        DeleteBreedCommand command,
        CancellationToken cancellationToken = default)
    {
        if (await _readDbContext.Pets.AnyAsync(p => p.BreedId == command.BreedId, cancellationToken))
            return Errors.General.RelatedDataExists(command.BreedId).ToErrorList();

        var specieResult = await _speciesRepository.GetByIdAsync(command.SpecieId, cancellationToken);
        if (specieResult.IsFailure)
            return specieResult.Error.ToErrorList();

        var breedResult = specieResult.Value.GetBreed(command.BreedId);
        if (breedResult.IsFailure)
            return breedResult.Error.ToErrorList();

        specieResult.Value.RemoveBreed(breedResult.Value);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return breedResult.Value.Id;
    }
}