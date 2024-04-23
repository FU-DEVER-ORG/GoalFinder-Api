using GoalFinder.Application.Shared.Features;

namespace GoalFinder.Application.Features.Auth.Register;

/// <summary>
///     Register as user response.
/// </summary>
public sealed class RegisterAsUserResponse : IFeatureResponse
{
    public RegisterAsUserResponseStatusCode StatusCode { get; init; }
}
