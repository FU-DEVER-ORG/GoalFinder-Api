using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoalFinder.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoalFinder.MySqlRelationalDb.Repositories.GetAllReports;

internal sealed partial class GetAllReportsRepository
{
    public async Task<IEnumerable<MatchPlayer>> GetAllReportsQueryAsync(
        CancellationToken cancellationToken
    )
    {
        return await _matchPlayers
            .AsNoTracking()
            .OrderBy(keySelector: report => report.FootballMatch.EndTime)
            .Select(report => new MatchPlayer() { 
                MatchId = report.MatchId,
                PlayerId = report.PlayerId,
                NumberOfReports = report.NumberOfReports,
                UserDetail = new UserDetail()
                {
                    FirstName = report.UserDetail.FirstName,
                    LastName = report.UserDetail.LastName,
                }
            })
            .ToListAsync(cancellationToken: cancellationToken);
    }
}
