using GoalFinder.Data.Entities;
using GoalFinder.Data.Repositories.UpdateUserInfo;
using GoalFinder.MySqlRelationalDb.Data;
using Microsoft.EntityFrameworkCore;

namespace GoalFinder.MySqlRelationalDb.Repositories.UpdateUserInfo;

internal sealed partial class UpdateUserInfoRepository : IUpdateUserInfoRepository
{
    private readonly GoalFinderContext _context;
    private readonly DbSet<User> _users;
    private readonly DbSet<UserDetail> _userDetails;
    private readonly DbSet<CompetitionLevel> _competitionLevels;
    private readonly DbSet<Experience> _experiences;
    private readonly DbSet<Position> _positions;
    private readonly DbSet<UserPosition> _userPositions;
    private readonly DbSet<RefreshToken> _refreshTokens;

    internal UpdateUserInfoRepository(GoalFinderContext context)
    {
        _context = context;
        _users = _context.Set<User>();
        _userDetails = _context.Set<UserDetail>();
        _competitionLevels = _context.Set<CompetitionLevel>();
        _experiences = _context.Set<Experience>();
        _positions = _context.Set<Position>();
        _userPositions = _context.Set<UserPosition>();
        _refreshTokens = _context.Set<RefreshToken>();
    }
}
