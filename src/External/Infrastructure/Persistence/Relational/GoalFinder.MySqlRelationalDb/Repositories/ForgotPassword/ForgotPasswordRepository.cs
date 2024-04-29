using GoalFinder.Data.Entities;
using GoalFinder.Data.Repositories.ForgotPassword;
using GoalFinder.MySqlRelationalDb.Data;
using Microsoft.EntityFrameworkCore;

namespace GoalFinder.MySqlRelationalDb.Repositories.ForgotPassword;

/// <summary>
///     Forgot Password Repository
/// </summary>
internal sealed partial class ForgotPasswordRepository : IForgotPasswordRepository
{
    private readonly GoalFinderContext _context;
    private readonly DbSet<UserDetail> _userDetails;
    private readonly DbSet<UserToken> _userTokens;

    internal ForgotPasswordRepository(GoalFinderContext context)
    {
        _context = context;
        _userDetails = _context.Set<UserDetail>();
        _userTokens = _context.Set<UserToken>();
    }
}
