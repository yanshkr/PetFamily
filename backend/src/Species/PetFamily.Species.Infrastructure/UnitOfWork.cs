using Microsoft.EntityFrameworkCore.Storage;
using PetFamily.Core.Database;
using PetFamily.Species.Infrastructure.DbContexts;
using System.Data;

namespace PetFamily.Species.Infrastructure;
public class UnitOfWork(WriteDbContext dbContext) : IUnitOfWork
{
    public async Task<IDbTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);

        return transaction.GetDbTransaction();
    }
    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}