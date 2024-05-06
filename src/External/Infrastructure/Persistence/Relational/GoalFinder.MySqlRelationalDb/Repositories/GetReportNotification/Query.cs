using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoalFinder.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoalFinder.MySqlRelationalDb.Repositories.GetReportNotification;

internal partial class GetReportNotificationRepository
{
    public async Task<IEnumerable<FootballMatch>> GetMatchesWitUpperBlockTimeByUserId(
        DateTime currenTime,
        Guid userID,
        CancellationToken cancellationToken)
    {
        return await _matchPlayers
            .AsNoTracking()
            .Where(predicate: matchPlayer => 
                matchPlayer.PlayerId == userID 
                && matchPlayer.FootballMatch.EndTime <= currenTime.AddHours(-1))
            .OrderBy(keySelector: matchPlayer => matchPlayer.FootballMatch.EndTime)
            .Select(selector: matchPlayer => new FootballMatch
            {
                EndTime = matchPlayer.FootballMatch.EndTime,
                Id = matchPlayer.FootballMatch.Id,
            })
            .ToListAsync(cancellationToken);
    }

}
