using GoalFinder.Data.Repositories.UserDetail;
using GoalFinder.MySqlRelationalDb.Data;
using GoalFinder.MySqlRelationalDb.Repositories.Base;

namespace GoalFinder.MySqlRelationalDb.Repositories.UserDetail;

internal sealed partial class UserDetailRepository :
    BaseRepository<GoalFinder.Data.Entities.UserDetail>,
    IUserDetailRepository
{
    internal UserDetailRepository(GoalFinderContext context) : base(context: context)
    {
    }
}

