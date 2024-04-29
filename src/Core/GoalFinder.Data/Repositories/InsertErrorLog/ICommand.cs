using System;
using System.Threading;
using System.Threading.Tasks;

namespace GoalFinder.Data.Repositories.InsertErrorLog;

public partial interface IInsertErrorLogRepository
{
    Task<bool> InsertErrorLogCommandAsync(Exception exception, CancellationToken cancellationToken);
}
