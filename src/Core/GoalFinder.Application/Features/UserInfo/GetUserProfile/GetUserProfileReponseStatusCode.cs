namespace GoalFinder.Application.Features.UserInfo.GetUserProfile;

/// <summary>
///     Get User Profile Status Code
/// </summary>
public enum GetUserProfileResponseStatusCode
{
    USER_IS_NOT_FOUND,
    USER_IS_TEMPORARILY_REMOVED,
    INPUT_VALIDATION_FAIL,
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
    INPUT_NOT_UNDERSTANDABLE
}
