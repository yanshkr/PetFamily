using CSharpFunctionalExtensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PetFamily.Core.Abstraction;
using PetFamily.Core.Database;
using PetFamily.SharedKernel.ErrorManagement;
using PetFamily.SharedKernel.Structs;
using PetFamily.Species.Application.Commands.DeleteSpecie;
using PetFamily.Species.Domain.Ids;
using PetFamily.Volunteers.Contracts;

namespace PetFamily.Species.Application.Commands.DeleteBreed;
public class DeleteBreedHandler
    : ICommandHandler<PetBreedId, DeleteBreedCommand>
{
    private readonly ISpeciesRepository _speciesRepository;
    private readonly IVolunteersContract _volunteersContract;
    private readonly IReadDbContext _readDbContext;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteSpecieHandler> _logger;
    public DeleteBreedHandler(
        ISpeciesRepository speciesRepository,
        IVolunteersContract volunteersContract,
        IReadDbContext readDbContext,
        [FromKeyedServices(UnitOfWorkSelector.Species)] IUnitOfWork unitOfWork,
        ILogger<DeleteSpecieHandler> logger)
    {
        _speciesRepository = speciesRepository;
        _volunteersContract = volunteersContract;
        _readDbContext = readDbContext;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<PetBreedId, ErrorList>> HandleAsync(
        DeleteBreedCommand command,
        CancellationToken cancellationToken = default)
    {
        var petsExists = await _volunteersContract.IsPetsExistsWithBreedById(command.BreedId, cancellationToken);
        if (petsExists.IsSuccess)
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