using GoalFinder.Data.Entities;
using GoalFinder.Data.Repositories.GetAllReports;
using GoalFinder.MySqlRelationalDb.Data;
using Microsoft.EntityFrameworkCore;

namespace GoalFinder.MySqlRelationalDb.Repositories.GetAllReports;

/// <summary>
///     Get All Reports
/// </summary>
internal sealed partial class GetAllReportsRepository : IGetAllReportsRepository
{
    private readonly GoalFinderContext _context;
    private readonly DbSet<MatchPlayer> _matchPlayers;
    private readonly DbSet<FootballMatch> _footballMatch;

    internal GetAllReportsRepository(GoalFinderContext context)
    {
        _context = context;
        _matchPlayers = _context.Set<MatchPlayer>();
        _footballMatch = _context.Set<FootballMatch>();
    }
}
