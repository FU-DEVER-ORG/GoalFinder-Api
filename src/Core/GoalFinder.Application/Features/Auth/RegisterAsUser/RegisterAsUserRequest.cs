using GoalFinder.Application.Shared.Features;

namespace GoalFinder.Application.Features.Auth.Register;

/// <summary>
///     Register as user request.
/// </summary>
public sealed class RegisterAsUserRequest : IFeatureRequest<RegisterAsUserResponse>
{
    public string Email { get; init; }

    public string Password { get; init; }
}
