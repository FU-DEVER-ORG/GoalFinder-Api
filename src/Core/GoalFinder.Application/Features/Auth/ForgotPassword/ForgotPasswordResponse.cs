using GoalFinder.Application.Shared.Features;

namespace GoalFinder.Application.Features.Auth.ForgotPassword;

/// <summary>
///     Forgot Password Response.
/// </summary>
public sealed class ForgotPasswordResponse : IFeatureResponse
{
    public ForgotPasswordResponseStatusCode StatusCode {  get; init; }

    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public string OtpCode { get; init; }
    }
}
