using PetFamily.Application.Abstraction;

namespace PetFamily.Application.Features.Queries.Volunteers.GetVolunteerById;
public record GetVolunteerByIdQuery(Guid Id) : IQuery;