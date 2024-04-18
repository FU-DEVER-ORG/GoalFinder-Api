using GoalFinder.Data.Entities.Base;
using GoalFinder.Data.Repositories.Base;
using GoalFinder.MySqlRelationalDb.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GoalFinder.MySqlRelationalDb.Repositories.Base;

/// <summary>
///     Implementation of base repository.
/// </summary>
/// <typeparam name="TEntity">
///     Entity type.
/// </typeparam>
internal abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
    where TEntity :
        class,
        IBaseEntity
{
    protected readonly DbSet<TEntity> _dbSet;

    private protected BaseRepository(GoalFinderContext context)
    {
        _dbSet = context.Set<TEntity>();
    }

    public async Task AddAsync(
        TEntity newEntity,
        CancellationToken cancellationToken)
    {
        await _dbSet.AddAsync(
            entity: newEntity,
            cancellationToken: cancellationToken);
    }

    public Task AddRangeAsync(
        IEnumerable<TEntity> newEntities,
        CancellationToken cancellationToken)
    {
        return _dbSet.AddRangeAsync(
            entities: newEntities,
            cancellationToken: cancellationToken);
    }
}
