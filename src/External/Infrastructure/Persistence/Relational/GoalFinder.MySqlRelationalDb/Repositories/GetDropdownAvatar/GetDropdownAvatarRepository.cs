using GoalFinder.Data.Entities;
using GoalFinder.Data.Repositories.GetDropdownAvatar;
using GoalFinder.MySqlRelationalDb.Data;
using Microsoft.EntityFrameworkCore;

namespace GoalFinder.MySqlRelationalDb.Repositories.GetDropdownAvatar;

internal sealed partial class GetDropdownAvatarRepository : IGetDropdownAvatarRepository
{
    private readonly GoalFinderContext _context;
    private readonly DbSet<UserDetail> _userDetails;

    internal GetDropdownAvatarRepository(GoalFinderContext context)
    {
        _context = context;
        _userDetails = _context.Set<UserDetail>();
    }
}
