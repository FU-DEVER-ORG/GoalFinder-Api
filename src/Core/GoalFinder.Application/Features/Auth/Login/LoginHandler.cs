using GoalFinder.Application.Shared.Features;
using System.Threading;
using System.Threading.Tasks;

namespace GoalFinder.Application.Features.Auth.Login;

/// <summary>
///     Login request handler.
/// </summary>
public sealed class LoginHandler : IFeatureHandler<LoginRequest, LoginResponse>
{
    public Task<LoginResponse> ExecuteAsync(
        LoginRequest command,
        CancellationToken ct)
    {
        LoginResponse response = new()
        {
            Message = "Hello world"
        };

        return Task.FromResult(response);
    }
}
