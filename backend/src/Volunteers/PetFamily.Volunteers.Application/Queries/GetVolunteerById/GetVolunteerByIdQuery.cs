using PetFamily.Core.Abstraction;

namespace PetFamily.Volunteers.Application.Queries.GetVolunteerById;
public record GetVolunteerByIdQuery(Guid Id) : IQuery;