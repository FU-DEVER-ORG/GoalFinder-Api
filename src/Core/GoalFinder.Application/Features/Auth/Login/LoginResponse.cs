using GoalFinder.Application.Shared.Features;

namespace GoalFinder.Application.Features.Auth.Login;

/// <summary>
///     Login response.
/// </summary>
public sealed class LoginResponse : IFeatureResponse
{
    public string Message { get; set; }
}
