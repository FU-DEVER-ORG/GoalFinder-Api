using GoalFinder.Data.Repositories.InsertErrorLog;
using GoalFinder.MySqlRelationalDb.Data;

namespace GoalFinder.MySqlRelationalDb.Repositories.InsertErrorLog;

/// <summary>
///     Implementation of IInsertErrorLogRepository
/// </summary>
internal sealed partial class InsertErrorLogRepository : IInsertErrorLogRepository
{
    private readonly GoalFinderContext _context;

    internal InsertErrorLogRepository(GoalFinderContext context)
    {
        _context = context;
    }
}
