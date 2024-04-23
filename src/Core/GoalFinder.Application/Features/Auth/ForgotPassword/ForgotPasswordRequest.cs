using GoalFinder.Application.Shared.Features;

namespace GoalFinder.Application.Features.Auth.ForgotPassword;

public sealed class ForgotPasswordRequest : IFeatureRequest<ForgotPasswordResponse>
{
    public string UserName { get; init; }
}
