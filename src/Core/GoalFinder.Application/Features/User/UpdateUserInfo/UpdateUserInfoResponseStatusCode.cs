namespace GoalFinder.Application.Features.User.UpdateUserInfo;

/// <summary>
///     Update user info response status code.
/// </summary>
public enum UpdateUserInfoResponseStatusCode
{
    UPDATE_SUCCESS,
    USER_NOT_FOUND,
    DATABASE_OPERATION_FAIL,
    USERNAME_IS_ALREADY_TAKEN,
    INPUT_VALIDATION_FAIL,
    UN_AUTHORIZED,
    FORBIDDEN,
    USER_IS_TEMPORARILY_REMOVED,
    INPUT_NOT_UNDERSTANDABLE
}
