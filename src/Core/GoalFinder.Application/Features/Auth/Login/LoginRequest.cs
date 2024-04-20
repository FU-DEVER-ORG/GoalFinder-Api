using GoalFinder.Application.Shared.Features;

namespace GoalFinder.Application.Features.Auth.Login;

/// <summary>
///     Login request.
/// </summary>
public sealed class LoginRequest : IFeatureRequest<LoginResponse>
{
    public string Username { get; init; }

    public string Password { get; init; }

    public bool IsRemember { get; init; }
}
