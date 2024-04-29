using System;
using System.Threading;
using System.Threading.Tasks;

namespace GoalFinder.Data.Repositories.ReportUserAfterMatch;

/// <summary>
///      Command interface for report user after match
/// </summary>
public partial interface IReportUserAfterMatchRepository
{
    Task<bool> ReportUserAfterMatchCommandAsync(
        int bonusAfterMatch,
        Guid playerId,
        CancellationToken cancellationToken
    );
}
