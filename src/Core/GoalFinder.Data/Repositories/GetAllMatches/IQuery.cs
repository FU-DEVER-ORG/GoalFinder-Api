using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GoalFinder.Data.Entities;

namespace GoalFinder.Data.Repositories.GetAllMatches;

/// <summary>
///     Interface for Get All Football Matches Repository.
/// </summary>
public partial interface IGetAllMatchesRepository
{
    Task<IEnumerable<FootballMatch>> GetAllMatchesQueryAsync(CancellationToken cancellationToken);
}
