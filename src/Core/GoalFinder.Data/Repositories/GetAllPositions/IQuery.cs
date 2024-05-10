using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GoalFinder.Data.Entities;

namespace GoalFinder.Data.Repositories.GetAllPositions;

/// <summary>
///     Interface for Get All Positions Repository.
/// </summary>
public partial interface IGetAllPositionsRepository
{
    Task<IEnumerable<Position>> GetAllPositionsQueryAsync(CancellationToken cancellationToken);
}
