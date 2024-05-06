using GoalFinder.Data.Entities;
using GoalFinder.Data.Repositories.GetAllExperiences;
using GoalFinder.MySqlRelationalDb.Data;
using Microsoft.EntityFrameworkCore;

namespace GoalFinder.MySqlRelationalDb.Repositories.GetAllExperiences;

/// <summary>
///     Get All Experiences
/// </summary>
internal sealed partial class GetAllExperiencesRepository : IGetAllExperiencesRepository
{
    private readonly GoalFinderContext _context;
    private readonly DbSet<Experience> _experiences;

    internal GetAllExperiencesRepository(GoalFinderContext context)
    {
        _context = context;
        _experiences = _context.Set<Experience>();
    }
}
