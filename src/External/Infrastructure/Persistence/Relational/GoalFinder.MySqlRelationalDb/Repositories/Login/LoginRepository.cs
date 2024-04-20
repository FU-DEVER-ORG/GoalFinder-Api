using GoalFinder.MySqlRelationalDb.Data;
using GoalFinder.Data.Repositories.Login;
using Microsoft.EntityFrameworkCore;
using GoalFinder.Data.Entities;

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
