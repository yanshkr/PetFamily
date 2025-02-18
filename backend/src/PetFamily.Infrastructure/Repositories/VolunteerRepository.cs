using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.Application.Features.Commands.Volunteers;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteers;
using PetFamily.Domain.Volunteers.Ids;
using PetFamily.Infrastructure.DbContexts;

namespace PetFamily.Infrastructure.Repositories;
public class VolunteerRepository : IVolunteersRepository
{
    private readonly WriteDbContext _dbContext;

    public VolunteerRepository(WriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<VolunteerId> AddAsync(Volunteer volunteer, CancellationToken cancellationToken = default)
    {
        await _dbContext.Volunteers.AddAsync(volunteer, cancellationToken);

        return volunteer.Id;
    }
    public async Task<Result<Volunteer, Error>> GetByIdAsync(VolunteerId id, CancellationToken cancellationToken = default)
    {
        var volunteer = await _dbContext.Volunteers
            .Include(v => v.Pets)
            .FirstOrDefaultAsync(v => v.Id == id.Value, cancellationToken);

        if (volunteer is null)
            return Errors.General.NotFound(id);

        return volunteer;
    }
    public Result<VolunteerId> Delete(Volunteer volunteer)
    {
        _dbContext.Volunteers.Remove(volunteer);
        return volunteer.Id;
    }
}