using GoalFinder.Application.Shared.Features;
using System.Threading;
using System.Threading.Tasks;

namespace GoalFinder.Application.Features.Auth.RefreshAccessToken;

/// <summary>
///     Handler for RefreshAccessToken
/// </summary>

internal class RefreshAccessTokenHandler :
    IFeatureHandler<RefreshAccessTokenRequest, RefreshAccessTokenResponse>
{
    public Task<RefreshAccessTokenResponse> ExecuteAsync(RefreshAccessTokenRequest command, CancellationToken ct)
    {
        throw new System.NotImplementedException();
    }
}
