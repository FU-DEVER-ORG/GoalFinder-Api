using System.Threading.Tasks;
using System.Threading;
using GoalFinder.Data.Entities;
using System.Collections.Generic;

namespace GoalFinder.Data.Repositories.GetAllMatches;

/// <summary>
///     Interface for Get All Football Matches Repository.
/// </summary>
public partial interface IGetAllMatchesRepository
{
    Task<IEnumerable<FootballMatch>> GetAllMatchesQueryAsync(
        CancellationToken cancellationToken);
}