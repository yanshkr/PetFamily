using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteers;
using PetFamily.Domain.Volunteers.Ids;

namespace PetFamily.Application.Features.Commands.Volunteers;
public interface IVolunteersRepository
{
    Task<VolunteerId> AddAsync(Volunteer volunteer, CancellationToken cancellationToken = default);
    Task<Result<Volunteer, Error>> GetByIdAsync(VolunteerId id, CancellationToken cancellationToken = default);
    Result<VolunteerId> Delete(Volunteer volunteer);
}
