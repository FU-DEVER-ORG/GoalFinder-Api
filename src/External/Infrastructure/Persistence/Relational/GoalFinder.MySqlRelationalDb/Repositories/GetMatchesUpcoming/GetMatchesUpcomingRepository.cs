using GoalFinder.Data.Entities;
using GoalFinder.Data.Repositories.GetMatchesUpcoming;
using GoalFinder.MySqlRelationalDb.Data;
using Microsoft.EntityFrameworkCore;

namespace GoalFinder.MySqlRelationalDb.Repositories.GetMatchesUpcoming;

internal sealed partial class GetMatchesUpcomingRepository : IGetMatchesUpcomingRepository
{
    private readonly GoalFinderContext _context;
    private readonly DbSet<UserDetail> _userDetails;
    private readonly DbSet<RefreshToken> _refreshTokens;
    internal GetMatchesUpcomingRepository(GoalFinderContext context)
    {
        _context = context;
        _userDetails = _context.Set<UserDetail>();
        _refreshTokens = _context.Set<RefreshToken>();
    }

    
}
