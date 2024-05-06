using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GoalFinder.Data.Entities;

namespace GoalFinder.Data.Repositories.GetReportNotification;

/// <summary>
///     Interface for Get All Football Matches Repository.
/// </summary>
public partial interface IGetReportNotificationRepository
{
    Task<
        IEnumerable<(MatchPlayer Player, FootballMatch Match)>
    > GetAllNotificationReportWithUpperBlockTimeByUserId(
        DateTime currenTime,
        Guid userID,
        CancellationToken cancellationToken
    );
}
