using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFamily.Application.Abstraction;
public interface IQueryHandler<TResponse, in TQuery> where TQuery : IQuery
{
    public Task<TResponse> HandleAsync(TQuery query, CancellationToken cancellationToken = default);
}