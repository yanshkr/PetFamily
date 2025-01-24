using CSharpFunctionalExtensions;
using PetFamily.Application.Features.Volunteers;
using PetFamily.Domain.Entities;
using PetFamily.Domain.Shared;

namespace PetFamily.Infrastructure.Repositories;
public class VolunteerRepository : IVolunteersRepository
{
    private readonly ApplicationDbContext _dbContext;

    public VolunteerRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> AddAsync(Volunteer volunteer, CancellationToken cancellationToken = default)
    {
        await _dbContext.Volunteers.AddAsync(volunteer, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return volunteer.Id;
    }

    public async Task<Result<Volunteer, Error>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var volunteer = await _dbContext.Volunteers.FindAsync([id], cancellationToken);

        if (volunteer is null)
            return Errors.General.NotFound(id);

        return volunteer;
    }
}
