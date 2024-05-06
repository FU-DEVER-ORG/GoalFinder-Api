using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoalFinder.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoalFinder.MySqlRelationalDb.Repositories.GetAllCompetitionLevels;

/// <summary>
///     Implementation of <see cref="IGetAllCompetitionLevelsQuery"/>
/// </summary>
internal sealed partial class GetAllCompetitionLevelsRepository
{
    public async Task<IEnumerable<CompetitionLevel>> GetAllCompetitionLevelsQueryAsync(
        CancellationToken cancellationToken
    )
    {
        return await _competitionLevels
            .AsNoTracking()
            .Select(competitionLevel => new CompetitionLevel
            {
                Id = competitionLevel.Id,
                FullName = competitionLevel.FullName,
            })
            .ToListAsync(cancellationToken: cancellationToken);
    }
}
