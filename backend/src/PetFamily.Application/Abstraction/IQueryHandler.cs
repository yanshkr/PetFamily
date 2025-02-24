namespace PetFamily.Application.Abstraction;
public interface IQueryHandler<TResponse, in TQuery> where TQuery : IQuery
{
    public Task<TResponse> HandleAsync(TQuery query, CancellationToken cancellationToken = default);
}