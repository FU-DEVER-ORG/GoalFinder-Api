using GoalFinder.Data.Entities;
using GoalFinder.Data.Repositories.GetReportNotification;
using GoalFinder.MySqlRelationalDb.Data;
using Microsoft.EntityFrameworkCore;

namespace GoalFinder.MySqlRelationalDb.Repositories.GetReportNotification;

internal sealed partial class GetReportNotificationRepository : IGetReportNotificationRepository
{
    private readonly GoalFinderContext _context;
    private readonly DbSet<MatchPlayer> _matchPlayers;

    internal GetReportNotificationRepository(GoalFinderContext context)
    {
        _context = context;
        _matchPlayers = _context.Set<MatchPlayer>();
    }
}
