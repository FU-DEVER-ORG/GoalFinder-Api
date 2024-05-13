using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoalFinder.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoalFinder.MySqlRelationalDb.Repositories.GetAllReports;

internal sealed partial class GetAllReportsRepository
{

    public async Task<IEnumerable<MatchPlayer>> GetMatchPlayerByMatchIdAndUserIdAsync(
        Guid matchId,
        Guid userId,
        CancellationToken cancellationToken
    )
    {
        return await _matchPlayers
            .AsNoTracking()
            .Where(matchPlayer => matchPlayer.MatchId == matchId && matchPlayer.PlayerId != userId)
            .Select(matchPlayer => new MatchPlayer
            {
                PlayerId = matchPlayer.PlayerId,
                NumberOfReports = matchPlayer.NumberOfReports
            })
            .ToListAsync(cancellationToken);
    }



    public Task<FootballMatch> GetFootballMatchByIdQueryAsync(
        Guid matchId,
        CancellationToken cancellationToken
    )
    {
        return _footballMatch
            .AsNoTracking()
            .Where(predicate: footballMatch => footballMatch.Id == matchId)
            .Select(selector: footballMatch => new FootballMatch
            {
                PitchAddress = footballMatch.PitchAddress,
                MaxMatchPlayersNeed = footballMatch.MaxMatchPlayersNeed,
                PitchPrice = footballMatch.PitchPrice,
                Description = footballMatch.Description,
                StartTime = footballMatch.StartTime,
                EndTime = footballMatch.EndTime,
                Address = footballMatch.Address,
                CompetitionLevel = new() { FullName = footballMatch.CompetitionLevel.FullName, },
            })
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }
}
