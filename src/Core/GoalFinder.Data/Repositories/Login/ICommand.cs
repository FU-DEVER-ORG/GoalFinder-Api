using System.Threading;
using System.Threading.Tasks;
using GoalFinder.Data.Entities;

namespace GoalFinder.Data.Repositories.Login;

public partial interface ILoginRepository
{
    Task<bool> CreateRefreshTokenCommandAsync(
        RefreshToken refreshToken,
        CancellationToken cancellationToken
    );
}
