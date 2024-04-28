using GoalFinder.Data.Entities;
using GoalFinder.Data.Repositories.Login;
using GoalFinder.MySqlRelationalDb.Data;
using Microsoft.EntityFrameworkCore;

namespace GoalFinder.MySqlRelationalDb.Repositories.Login;

/// <summary>
///     Implementation of ILoginRepository
/// </summary>
internal sealed partial class LoginRepository : ILoginRepository
{
    private readonly GoalFinderContext _context;
    private readonly DbSet<UserDetail> _userDetails;

    internal LoginRepository(GoalFinderContext context)
    {
        _context = context;
        _userDetails = _context.Set<UserDetail>();
    }
}
