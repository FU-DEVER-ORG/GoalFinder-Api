using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GoalFinder.Data.Entities;

namespace GoalFinder.Data.Repositories.GetAllCompetitionLevels;

/// <summary>
///     Interface for Get All CompetitionLevels Repository.
/// </summary>
public partial interface IGetAllCompetitionLevelsRepository
{
    Task<IEnumerable<CompetitionLevel>> GetAllCompetitionLevelsQueryAsync(CancellationToken cancellationToken);
}
