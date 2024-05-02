using System;
using System.Threading;
using System.Threading.Tasks;
using GoalFinder.Data.Entities;
using GoalFinder.Data.Repositories.CreateMatch;
using GoalFinder.MySqlRelationalDb.Data;
using Microsoft.EntityFrameworkCore;

namespace GoalFinder.MySqlRelationalDb.Repositories.CreateMatch;

/// <summary>
///     Create Match Repository
/// </summary>
internal sealed partial class CreateMatchRepository : ICreateMatchRepository
{
    private readonly GoalFinderContext _context;
    private readonly DbSet<FootballMatch> _footballMatches;
    private readonly DbSet<UserDetail> _userDetails;
    private readonly DbSet<CompetitionLevel> _competitionLevels;
    private readonly DbSet<RefreshToken> _refreshTokens;
    private readonly DbSet<MatchPlayer> _matchPlayers;
    internal CreateMatchRepository(GoalFinderContext context)
    {
        _context = context;
        _footballMatches = _context.Set<FootballMatch>();
        _userDetails = _context.Set<UserDetail>();
        _competitionLevels = _context.Set<CompetitionLevel>();
        _matchPlayers = _context.Set<MatchPlayer>();
        _refreshTokens = _context.Set<RefreshToken>();
    }
}
