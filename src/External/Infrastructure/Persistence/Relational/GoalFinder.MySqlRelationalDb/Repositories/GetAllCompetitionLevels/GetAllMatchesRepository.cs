using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GoalFinder.Data.Entities;
using GoalFinder.Data.Repositories.GetAllCompetitionLevels;
using GoalFinder.MySqlRelationalDb.Data;
using Microsoft.EntityFrameworkCore;

namespace GoalFinder.MySqlRelationalDb.Repositories.GetAllCompetitionLevels;

/// <summary>
///     Get All CompetitionLevels
/// </summary>
internal sealed partial class GetAllCompetitionLevelsRepository : IGetAllCompetitionLevelsRepository
{
    private readonly GoalFinderContext _context;
    private readonly DbSet<CompetitionLevel> _competitionLevels;

    internal GetAllCompetitionLevelsRepository(GoalFinderContext context)
    {
        _context = context;
        _competitionLevels = _context.Set<CompetitionLevel>();
    }
}
