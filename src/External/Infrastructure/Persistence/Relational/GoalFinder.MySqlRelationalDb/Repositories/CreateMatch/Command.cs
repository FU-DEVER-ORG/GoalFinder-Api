using System;
using System.Threading;
using System.Threading.Tasks;
using GoalFinder.Data.Entities;

namespace GoalFinder.MySqlRelationalDb.Repositories.CreateMatch;

internal sealed partial class CreateMatchRepository
{
    public async Task<bool> CreateMatchCommandAsync(
        Guid userId,
        FootballMatch footballMatch,
        MatchPlayer matchPlayer,
        CancellationToken cancellationToken
    )
    {
        try
        {
            _footballMatches.Add(entity: footballMatch);
            _matchPlayers.Add(entity: matchPlayer);

            await _context.SaveChangesAsync(cancellationToken: cancellationToken);
        }
        catch
        {
            return false;
        }

        return true;
    }
}
