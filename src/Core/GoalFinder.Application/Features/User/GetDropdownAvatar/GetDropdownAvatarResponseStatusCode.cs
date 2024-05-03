namespace GoalFinder.Application.Features.User.GetDropdownAvatar;

/// <summary>
///     Feature get dropdown avatar response status code.
/// </summary>
public enum GetDropdownAvatarResponseStatusCode
{
    FORBIDDEN,
    OPERATION_SUCCESS,
    USER_IS_NOT_FOUND,
    DATABASE_OPERATION_FAIL,
}
