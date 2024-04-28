using GoalFinder.Data.Repositories.RefreshAccessTokenRepository;
using GoalFinder.MySqlRelationalDb.Data;

namespace GoalFinder.MySqlRelationalDb.Repositories.RefreshAccessTokenRepository;

internal partial class RefreshAccessTokenRepository : IRefreshAccessTokenRepository
{
    private readonly GoalFinderContext _context;

    public RefreshAccessTokenRepository(GoalFinderContext context)
    {
        _context = context;
    }
}