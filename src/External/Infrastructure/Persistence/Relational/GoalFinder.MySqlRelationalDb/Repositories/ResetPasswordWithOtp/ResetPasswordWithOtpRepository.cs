using GoalFinder.Data.Repositories.ResetPasswordWithOtp;
using GoalFinder.MySqlRelationalDb.Data;
using Microsoft.EntityFrameworkCore;

namespace GoalFinder.MySqlRelationalDb.Repositories.ResetPasswordWithOtp;

/// <summary>
///     Implement <see cref="IResetPasswordWithOtpRepository"/>
/// </summary>

internal sealed partial class ResetPasswordWithOtpRepository : IResetPasswordWithOtpRepository
{
    private readonly GoalFinderContext _context;
    private readonly DbSet<GoalFinder.Data.Entities.UserToken> _userTokens;
    private readonly DbSet<GoalFinder.Data.Entities.UserDetail> _userDetails;

    public ResetPasswordWithOtpRepository(GoalFinderContext context)
    {
        _context = context;
        _userTokens = _context.Set<GoalFinder.Data.Entities.UserToken>();
        _userDetails = _context.Set<GoalFinder.Data.Entities.UserDetail>();
    }
}
