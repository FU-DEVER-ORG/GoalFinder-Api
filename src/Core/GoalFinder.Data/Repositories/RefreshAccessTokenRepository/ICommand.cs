using System;
using System.Threading;
using System.Threading.Tasks;
using GoalFinder.Data.Entities;

namespace GoalFinder.Data.Repositories.RefreshAccessTokenRepository;

public partial interface IRefreshAccessTokenRepository
{
    Task<bool> CreateRefreshTokenCommandAsync(
        RefreshToken refreshToken,
        CancellationToken cancellationToken
    );

    Task<bool> DeleteRefreshTokenCommandAsync(
        Guid accessTokenId,
        CancellationToken cancellationToken
    );
}
