namespace GoalFinder.Application.Features.User.GetUserInfoOnSidebar;

/// <summary>
///     Feature response status code.
/// </summary>
public enum GetUserInfoOnSidebarResponseStatusCode
{
    OPERATION_SUCCESS,
    USER_IS_NOT_FOUND,
    DATABASE_OPERATION_FAIL,
    USER_IS_TEMPORARILY_REMOVED,
    FORBIDDEN
}
