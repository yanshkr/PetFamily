using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetFamily.Application.Abstraction;
using PetFamily.Application.Database;
using PetFamily.Application.Features.Commands.Species.DeleteBreed;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Species.Ids;

namespace PetFamily.Application.Features.Commands.Species.Delete;
public class DeleteSpecieHandler
    : ICommandHandler<PetSpecieId, DeleteSpecieCommand>
{
    private readonly ISpeciesRepository _speciesRepository;
    private readonly IReadDbContext _readDbContext;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteSpecieHandler> _logger;
    public DeleteSpecieHandler(
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

    public async Task<Result<PetSpecieId, ErrorList>> HandleAsync(
        DeleteSpecieCommand command,
        CancellationToken cancellationToken = default)
    {
        if (await _readDbContext.Pets.AnyAsync(p => p.SpecieId == command.Id, cancellationToken))
            return Errors.General.RelatedDataExists(command.Id).ToErrorList();

        var specieResult = await _speciesRepository.GetByIdAsync(command.Id, cancellationToken);

        if (specieResult.IsFailure)
            return specieResult.Error.ToErrorList();

        _speciesRepository.Delete(specieResult.Value);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return specieResult.Value.Id;
    }
}