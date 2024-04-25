
using GoalFinder.Data.Entities;
using GoalFinder.Data.Repositories.GetUserProfile;
using GoalFinder.MySqlRelationalDb.Data;
using Microsoft.EntityFrameworkCore;

namespace GoalFinder.MySqlRelationalDb.Repositories.GetUserProfile;

internal sealed partial class GetUserProfileRepository : IGetUserProfileRepository
{
    private readonly GoalFinderContext _context;
    private readonly DbSet<UserDetail> _userDetails;
    private readonly DbSet<MatchPlayer> _matchPlayer;
    private readonly DbSet<User> _users;

    internal GetUserProfileRepository(GoalFinderContext context )
    {
        _context = context;
        _userDetails = _context.Set<UserDetail>();
        _matchPlayer = _context.Set<MatchPlayer>();
        _users = _context.Set<User>();
    }
}