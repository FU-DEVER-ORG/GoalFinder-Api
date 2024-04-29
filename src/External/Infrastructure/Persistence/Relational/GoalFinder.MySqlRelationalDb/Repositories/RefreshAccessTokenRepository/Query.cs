using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoalFinder.Application.Shared.Commons;
using GoalFinder.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoalFinder.MySqlRelationalDb.Repositories.RefreshAccessTokenRepository;

/// <summary>
///    This is an auto generated class.
/// </summary>

internal partial class RefreshAccessTokenRepository
{
    public Task<bool> IsRefreshTokenFoundByAccessTokenIdQueryAsync(
        Guid accessTokenId,
        CancellationToken cancellationToken
    )
    {
        return _refreshTokens.AnyAsync(
            predicate: refreshToken => refreshToken.AccessTokenId == accessTokenId,
            cancellationToken: cancellationToken
        );
    }

    public Task<bool> IsUserTemporarilyRemovedQueryAsync(
        Guid userId,
        CancellationToken cancellationToken
    )
    {
        return _userDetails.AnyAsync(
            predicate: userDetail =>
                userDetail.UserId == userId
                && userDetail.RemovedBy != CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID
                && userDetail.RemovedAt != DateTime.MinValue,
            cancellationToken: cancellationToken
        );
    }

    public Task<RefreshToken> FindByRefreshTokenValueQueryAsync(
        string refreshTokenValue,
        CancellationToken cancellationToken
    )
    {
        return _refreshTokens
            .Where(refreshToken => refreshToken.RefreshTokenValue.Equals(refreshTokenValue))
            .Select(refreshToken => new RefreshToken()
            {
                RefreshTokenValue = refreshToken.RefreshTokenValue,
                AccessTokenId = refreshToken.AccessTokenId,
                ExpiredDate = refreshToken.ExpiredDate,
                UserId = refreshToken.UserId,
                CreatedAt = refreshToken.CreatedAt
            })
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }

    public Task<bool> IsRefreshTokenExpiredQueryAsync(
        Guid refreshTokenId,
        CancellationToken cancellationToken
    )
    {
        return _refreshTokens.AnyAsync(
            predicate: refreshToken =>
                refreshToken.AccessTokenId.Equals(refreshTokenId)
                && refreshToken.ExpiredDate < DateTime.UtcNow,
            cancellationToken: cancellationToken
        );
    }
}
