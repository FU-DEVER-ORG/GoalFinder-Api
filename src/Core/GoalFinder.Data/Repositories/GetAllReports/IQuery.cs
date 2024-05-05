using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GoalFinder.Data.Entities;

namespace GoalFinder.Data.Repositories.GetAllReports;

/// <summary>
///     Interface for get all reports
/// </summary>
public partial interface IGetAllReportsRepository
{
    Task<FootballMatch> GetFootballMatchByIdQueryAsync(
        Guid matchId,
        CancellationToken cancellationToken
    );

    Task<IEnumerable<MatchPlayer>> GetMatchPlayerByMatchIdAsync(
        Guid matchId,
        CancellationToken cancellationToken
    );
}
