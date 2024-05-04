using GoalFinder.Application.Features.User.ReportUserAfterMatch;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GoalFinder.Data.Repositories.ReportUserAfterMatch;

/// <summary>
///      Command interface for report user after match
/// </summary>
public partial interface IReportUserAfterMatchRepository
{
    Task<bool> ReportUserAfterMatchCommandAsync(
        List<PlayerPrestigeScore> playerScores,
        CancellationToken cancellationToken
    );
}
