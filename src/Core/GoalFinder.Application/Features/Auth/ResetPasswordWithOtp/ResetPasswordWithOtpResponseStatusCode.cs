namespace GoalFinder.Application.Features.Auth.ResetPasswordWithOtp;

/// <summary>
///     Reset password with otp response status code for <see cref="ResetPasswordWithOtpResponse"/>
/// </summary>

public enum ResetPasswordWithOtpResponseStatusCode
{
    INPUT_VALIDATION_FAILD,
    OTP_CODE_NOT_VALID,
    OTP_CODE_IS_EXPIRED,
    OTP_CODE_NOT_FOUND,
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAILD,
    NEW_PASSWORD_NOT_MATCH_CONFIRM_PASSWORD,
    USER_IS_TEMPORARY_REMOVED
}
