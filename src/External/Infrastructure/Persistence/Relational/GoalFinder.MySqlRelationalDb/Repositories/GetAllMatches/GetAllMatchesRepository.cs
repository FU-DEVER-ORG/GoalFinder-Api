
using GoalFinder.Data.Entities;
using GoalFinder.Data.Repositories.GetAllMatches;
using GoalFinder.MySqlRelationalDb.Data;
using Microsoft.EntityFrameworkCore;

namespace GoalFinder.MySqlRelationalDb.Repositories.GetAllMatches;

/// <summary>
///     Get All Football Matches
/// </summary>
internal sealed partial class GetAllMatchesRepository : IGetAllMatchesRepository
{
    private readonly GoalFinderContext _context;
    private readonly DbSet<FootballMatch> _footballMatch;

    internal GetAllMatchesRepository(GoalFinderContext context)
    {
        _context = context;
        _footballMatch = _context.Set<FootballMatch>();
    }

}
