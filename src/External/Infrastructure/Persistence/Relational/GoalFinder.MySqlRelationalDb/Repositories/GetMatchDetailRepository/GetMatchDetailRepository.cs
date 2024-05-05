using GoalFinder.Data.Entities;
using GoalFinder.Data.Repositories.GetMatchDetailRepository;
using GoalFinder.MySqlRelationalDb.Data;
using Microsoft.EntityFrameworkCore;

namespace GoalFinder.MySqlRelationalDb.Repositories.GetMatchDetailRepository;

internal sealed partial class GetMatchDetailRepository : IGetMatchDetailRepository
{
    private readonly GoalFinderContext _context;
    private readonly DbSet<UserDetail> _userDetails;
    private readonly DbSet<RefreshToken> _refreshTokens;
    private readonly DbSet<FootballMatch> _footballMatches;

    public GetMatchDetailRepository(GoalFinderContext context)
    {
        _context = context;
        _userDetails = _context.Set<UserDetail>();
        _refreshTokens = _context.Set<RefreshToken>();
        _footballMatches = _context.Set<FootballMatch>();
    }
}
