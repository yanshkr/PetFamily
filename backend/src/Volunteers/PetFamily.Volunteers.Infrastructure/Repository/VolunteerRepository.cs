using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using PetFamily.SharedKernel.ErrorManagement;
using PetFamily.Volunteers.Application;
using PetFamily.Volunteers.Domain.Entities;
using PetFamily.Volunteers.Domain.Ids;
using PetFamily.Volunteers.Infrastructure.DbContexts;

namespace PetFamily.Volunteers.Infrastructure.Repository;
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