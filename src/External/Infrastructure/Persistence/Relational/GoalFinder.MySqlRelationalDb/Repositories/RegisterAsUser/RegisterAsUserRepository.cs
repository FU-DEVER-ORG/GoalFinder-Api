using GoalFinder.Data.Repositories.RegisterAsUser;
using GoalFinder.MySqlRelationalDb.Data;

namespace GoalFinder.MySqlRelationalDb.Repositories.RegisterAsUser;

internal sealed partial class RegisterAsUserRepository : IRegisterAsUserRepository
{
    private readonly GoalFinderContext _context;

    internal RegisterAsUserRepository(GoalFinderContext context)
    {
        _context = context;
    }
}
