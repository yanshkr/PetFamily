using CSharpFunctionalExtensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PetFamily.Core.Abstraction;
using PetFamily.Core.Database;
using PetFamily.SharedKernel.ErrorManagement;
using PetFamily.SharedKernel.Structs;
using PetFamily.Species.Domain.Ids;
using PetFamily.Volunteers.Contracts;

namespace PetFamily.Species.Application.Commands.DeleteSpecie;
public class DeleteSpecieHandler
    : ICommandHandler<PetSpecieId, DeleteSpecieCommand>
{
    private readonly ISpeciesRepository _speciesRepository;
    private readonly IVolunteersContract _volunteersContract;
    private readonly IReadDbContext _readDbContext;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteSpecieHandler> _logger;
    public DeleteSpecieHandler(
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

    public async Task<Result<PetSpecieId, ErrorList>> HandleAsync(
        DeleteSpecieCommand command,
        CancellationToken cancellationToken = default)
    {
        var petsExists = await _volunteersContract.IsPetsExistsWithSpecieById(command.Id, cancellationToken);
        if (petsExists.IsSuccess)
            return Errors.General.RelatedDataExists(command.Id).ToErrorList();

        var specieResult = await _speciesRepository.GetByIdAsync(command.Id, cancellationToken);

        if (specieResult.IsFailure)
            return specieResult.Error.ToErrorList();

        _speciesRepository.Delete(specieResult.Value);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return specieResult.Value.Id;
    }
}