using GoalFinder.Data.Entities;
using GoalFinder.Data.Repositories.RefreshAccessTokenRepository;
using GoalFinder.MySqlRelationalDb.Data;
using Microsoft.EntityFrameworkCore;

namespace GoalFinder.MySqlRelationalDb.Repositories.RefreshAccessTokenRepository;

/// <summary>
///    This is an auto generated class.
/// </summary>

internal partial class RefreshAccessTokenRepository : IRefreshAccessTokenRepository
{
    private readonly GoalFinderContext _context;
    private readonly DbSet<RefreshToken> _refreshTokens;
    private readonly DbSet<UserDetail> _userDetails;

    public RefreshAccessTokenRepository(GoalFinderContext context)
    {
        _context = context;
        _refreshTokens = _context.Set<RefreshToken>();
        _userDetails = _context.Set<UserDetail>();
    }
}
