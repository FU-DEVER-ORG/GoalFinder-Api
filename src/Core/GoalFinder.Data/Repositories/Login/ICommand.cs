using GoalFinder.Data.Entities;
using System.Threading.Tasks;
using System.Threading;

namespace GoalFinder.Data.Repositories.Login;

public partial interface ILoginRepository
{
    Task<bool> CreateRefreshTokenCommandAsync(
        RefreshToken refreshToken,
        CancellationToken cancellationToken);
}
