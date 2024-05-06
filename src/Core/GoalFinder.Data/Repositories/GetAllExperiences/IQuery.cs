using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GoalFinder.Data.Entities;

namespace GoalFinder.Data.Repositories.GetAllExperiences;

/// <summary>
///     Interface for Get All Experiences Repository.
/// </summary>
public partial interface IGetAllExperiencesRepository
{
    Task<IEnumerable<Experience>> GetAllExperiencesQueryAsync(CancellationToken cancellationToken);
}
