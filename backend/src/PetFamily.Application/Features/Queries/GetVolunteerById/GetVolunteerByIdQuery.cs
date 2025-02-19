using PetFamily.Application.Abstraction;

namespace PetFamily.Application.Features.Queries.GetVolunteerById;
public record GetVolunteerByIdQuery(Guid Id) : IQuery;