using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoalFinder.Application.Features.Notification.GetReportNotification;
using GoalFinder.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoalFinder.MySqlRelationalDb.Repositories.GetReportNotification;

internal partial class GetReportNotificationRepository
{
    public async Task<
        IEnumerable<(MatchPlayer Player, FootballMatch Match)>
    > GetAllNotificationReportWithUpperBlockTimeByUserId(
        DateTime currentTime,
        Guid userId,
        CancellationToken cancellationToken
    )
    {
        var notificationReports = await _matchPlayers
            .AsNoTracking()
            .Where(matchPlayer =>
                matchPlayer.PlayerId == userId
                && matchPlayer.FootballMatch.EndTime <= currentTime.AddHours(-1)
            )
            .OrderBy(matchPlayer => matchPlayer.FootballMatch.EndTime)
            .Select(matchPlayer => new GetReportNotificationResponse.ReportNotificationTuple
            {
                Player = new MatchPlayer { IsReported = matchPlayer.IsReported },
                Match = new FootballMatch
                {
                    EndTime = matchPlayer.FootballMatch.EndTime,
                    Id = matchPlayer.FootballMatch.Id
                }
            })
            .ToListAsync(cancellationToken);

        return notificationReports.Select(report => (report.Player, report.Match));
    }
}
