using GoalFinder.Application.Shared.Features;

namespace GoalFinder.Application.Features.Auth.ForgotPassword;

public sealed class ForgotPasswordResponse : IFeatureResponse
{
    public ForgotPasswordReponseStatusCode StatusCode {  get; init; }
    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public string OtpCode { get; set; }
    }
}
