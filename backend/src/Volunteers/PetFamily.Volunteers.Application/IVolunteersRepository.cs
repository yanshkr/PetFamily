using CSharpFunctionalExtensions;
using PetFamily.SharedKernel.ErrorManagement;
using PetFamily.Volunteers.Domain.Entities;
using PetFamily.Volunteers.Domain.Ids;

namespace PetFamily.Volunteers.Application;
public interface IVolunteersRepository
{
    Task<VolunteerId> AddAsync(Volunteer volunteer, CancellationToken cancellationToken = default);
    Task<Result<Volunteer, Error>> GetByIdAsync(VolunteerId id, CancellationToken cancellationToken = default);
    Result<VolunteerId> Delete(Volunteer volunteer);
}
