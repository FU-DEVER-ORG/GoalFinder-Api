namespace GoalFinder.Application.Features.Auth.ResetPasswordWithOtp;

/// <summary>
///     Extension Method for <see cref="ResetPasswordWithOtpResponseStatusCode"/>
/// </summary>
public static class ExtensionMethod
{
    /// <summary>
    ///     Convert <see cref="ResetPasswordWithOtpResponseStatusCode"/> to <see cref="string"/>
    /// </summary>
    /// <param name="statusCode"></param>
    /// <returns></returns>
    public static string ToAppCode(this ResetPasswordWithOtpResponseStatusCode statusCode)
    {
        return $"{nameof(Auth)}.{nameof(ResetPasswordWithOtp)}.{statusCode}";
    }
}
