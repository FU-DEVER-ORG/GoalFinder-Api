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
    Task<IEnumerable<MatchPlayer>> GetAllReportsQueryAsync(CancellationToken cancellationToken);
}
