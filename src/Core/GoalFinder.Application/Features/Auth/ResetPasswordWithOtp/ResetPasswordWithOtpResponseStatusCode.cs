namespace GoalFinder.Application.Features.Auth.ResetPasswordWithOtp;

/// <summary>
///     Reset password with otp response status code for <see cref="ResetPasswordWithOtpResponse"/>
/// </summary>

public enum ResetPasswordWithOtpResponseStatusCode
{
    INPUT_VALIDATION_FAILED,
    OTP_CODE_NOT_VALID,
    OTP_CODE_IS_EXPIRED,
    OTP_CODE_NOT_FOUND,
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAILED,
    NEW_PASSWORD_NOT_MATCH_CONFIRM_PASSWORD,
    NEW_PASSWORD_CANT_BE_MATCH_WITH_OLD_PASSWORD,
    USER_IS_TEMPORARY_REMOVED,
    INPUT_NOT_UNDERSTANDABLE,
}
