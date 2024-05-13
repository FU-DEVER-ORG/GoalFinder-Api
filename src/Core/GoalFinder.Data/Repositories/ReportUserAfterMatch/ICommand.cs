
using GoalFinder.Application.Features.User.ReportUserAfterMatch;
using GoalFinder.Data.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GoalFinder.Data.Repositories.ReportUserAfterMatch;

/// <summary>
///      Command interface for report user after match
/// </summary>
public partial interface IReportUserAfterMatchRepository
{
    Task<List<UserDetail>> ReportUserAfterMatchCommandAsync(
        List<PlayerPrestigeScore> playerScores,
        CancellationToken cancellationToken
    );
}
