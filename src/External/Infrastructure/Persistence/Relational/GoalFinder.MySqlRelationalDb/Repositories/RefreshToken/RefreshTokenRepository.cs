using GoalFinder.Data.Repositories.RefreshToken;
using GoalFinder.MySqlRelationalDb.Data;
using GoalFinder.MySqlRelationalDb.Repositories.Base;

namespace GoalFinder.MySqlRelationalDb.Repositories.RefreshToken;

/// <summary>
///     Implementation of <see cref="IRefreshTokenRepository"/>
/// </summary>
internal sealed class RefreshTokenRepository :
    BaseRepository<GoalFinder.Data.Entities.RefreshToken>,
    IRefreshTokenRepository
{
    internal RefreshTokenRepository(GoalFinderContext context) : base(context: context)
    {
    }
}
