using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoalFinder.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoalFinder.MySqlRelationalDb.Repositories.GetAllPositions;

/// <summary>
///     Implementation of <see cref="IGetAllPositionsQuery"/>
/// </summary>
internal sealed partial class GetAllPositionsRepository
{
    public async Task<IEnumerable<Position>> GetAllPositionsQueryAsync(
        CancellationToken cancellationToken
    )
    {
        return await _positions
            .AsNoTracking()
            .Select(position => new Position { Id = position.Id, FullName = position.FullName, })
            .ToListAsync(cancellationToken: cancellationToken);
    }
}
