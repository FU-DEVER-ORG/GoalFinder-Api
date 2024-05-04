using System;
using System.Threading;
using System.Threading.Tasks;

namespace GoalFinder.Data.Repositories.RefreshAccessTokenRepository;

public partial interface IRefreshAccessTokenRepository
{
    Task<bool> UpdateRefreshTokenCommandAsync(
        Guid accessTokenId,
        string refreshTokenValue,
        CancellationToken cancellationToken
    );
}
