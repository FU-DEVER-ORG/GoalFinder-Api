using GoalFinder.Data.Entities;
using GoalFinder.Data.Repositories.GetAllPositions;
using GoalFinder.MySqlRelationalDb.Data;
using Microsoft.EntityFrameworkCore;

namespace GoalFinder.MySqlRelationalDb.Repositories.GetAllPositions;

/// <summary>
///     Get All Positions
/// </summary>
internal sealed partial class GetAllPositionsRepository : IGetAllPositionsRepository
{
    private readonly GoalFinderContext _context;
    private readonly DbSet<Position> _positions;

    internal GetAllPositionsRepository(GoalFinderContext context)
    {
        _context = context;
        _positions = _context.Set<Position>();
    }
}
