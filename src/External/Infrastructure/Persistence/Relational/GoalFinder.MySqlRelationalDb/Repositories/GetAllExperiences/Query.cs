using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoalFinder.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoalFinder.MySqlRelationalDb.Repositories.GetAllExperiences;

/// <summary>
///     Implementation of <see cref="IGetAllExperiencesQuery"/>
/// </summary>
internal sealed partial class GetAllExperiencesRepository
{
    public async Task<IEnumerable<Experience>> GetAllExperiencesQueryAsync(
        CancellationToken cancellationToken
    )
    {
        return await _experiences
            .AsNoTracking()
            .Select(experience => new Experience
            {
                Id = experience.Id,
                FullName = experience.FullName,
            })
            .ToListAsync(cancellationToken: cancellationToken);
    }
}
