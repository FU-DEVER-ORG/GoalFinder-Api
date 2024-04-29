using System;
using System.Threading;
using System.Threading.Tasks;

namespace GoalFinder.Data.Repositories.RefreshAccessTokenRepository;

public partial interface IRefreshAccessTokenRepository
{
    Task<bool> IsRefreshTokenFoundByAccessTokenIdQueryAsync(
        Guid accessTokenId,
        CancellationToken cancellationToken
    );
    Task<bool> IsUserTemporarilyRemovedQueryAsync(Guid userId, CancellationToken cancellationToken);
    Task<Data.Entities.RefreshToken> FindByRefreshTokenValueQueryAsync(
        string refreshTokenValue,
        CancellationToken cancellationToken
    );
}
