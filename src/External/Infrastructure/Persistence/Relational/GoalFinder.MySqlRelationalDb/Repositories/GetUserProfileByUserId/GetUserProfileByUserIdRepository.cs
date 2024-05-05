using GoalFinder.Data.Entities;
using GoalFinder.Data.Repositories.GetUserProfileByUserId;
using GoalFinder.MySqlRelationalDb.Data;
using Microsoft.EntityFrameworkCore;

namespace GoalFinder.MySqlRelationalDb.Repositories.GetUserProfileByUserId;

internal sealed partial class GetUserProfileByUserIdRepository : IGetUserProfileByUserIdRepository
{
    private readonly GoalFinderContext _context;
    private readonly DbSet<UserDetail> _userDetails;
    private readonly DbSet<MatchPlayer> _matchPlayer;
    private readonly DbSet<User> _users;

    internal GetUserProfileByUserIdRepository(GoalFinderContext context)
    {
        _context = context;
        _userDetails = _context.Set<UserDetail>();
        _matchPlayer = _context.Set<MatchPlayer>();
        _users = _context.Set<User>();
    }
}
