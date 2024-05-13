using GoalFinder.Data.Entities;
using GoalFinder.Data.Repositories.ReportUserAfterMatch;
using GoalFinder.MySqlRelationalDb.Data;
using Microsoft.EntityFrameworkCore;

namespace GoalFinder.MySqlRelationalDb.Repositories.ReportUserAfterMatch;

internal sealed partial class ReportUserAfterMatchRepository : IReportUserAfterMatchRepository
{
    private readonly GoalFinderContext _context;
    private readonly DbSet<FootballMatch> _footballMatch;
    private readonly DbSet<MatchPlayer> _matchPlayer;
    private readonly DbSet<UserDetail> _userDetails;
    private readonly DbSet<RefreshToken> _refreshTokens;

    internal ReportUserAfterMatchRepository(GoalFinderContext context)
    {
        _context = context;
        _footballMatch = context.Set<FootballMatch>();
        _matchPlayer = context.Set<MatchPlayer>();
        _userDetails = context.Set<UserDetail>();
        _refreshTokens = _context.Set<RefreshToken>();
    }
}
