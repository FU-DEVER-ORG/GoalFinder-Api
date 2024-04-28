using GoalFinder.Data.Entities;
using GoalFinder.Data.Repositories.GetUserInfoOnSidebar;
using GoalFinder.MySqlRelationalDb.Data;
using Microsoft.EntityFrameworkCore;

namespace GoalFinder.MySqlRelationalDb.Repositories.GetUserInfoOnSidebar;

internal sealed partial class GetUserInfoOnSidebarRepository : IGetUserInfoOnSidebarRepository
{
    private readonly GoalFinderContext _context;
    private readonly DbSet<UserDetail> _userDetails;
    private readonly DbSet<RefreshToken> _refreshTokens;

    internal GetUserInfoOnSidebarRepository(GoalFinderContext context)
    {
        _context = context;
        _userDetails = _context.Set<UserDetail>();
        _refreshTokens = _context.Set<RefreshToken>();
    }
}
