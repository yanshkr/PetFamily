using PetFamily.Application.Abstraction;

namespace PetFamily.Application.Features.Queries.Pets.GetPetById;
public record GetPetByIdQuery(Guid Id) : IQuery;
