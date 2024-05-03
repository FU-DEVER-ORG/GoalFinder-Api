namespace GoalFinder.Application.Features.Auth.RefreshAccessToken;

/// <summary>
///     Status code for RefreshAccessToken
/// </summary>

public enum RefreshAccessTokenResponseStatusCode
{
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAILED,
    USER_IS_TEMPORARILY_REMOVED,
    UN_AUTHORIZED,
    INPUT_VALIDATION_FAIL,
    REFRESH_TOKEN_IS_NOT_FOUND,
    REFRESH_TOKEN_IS_EXPIRED,
    FORBIDDEN
}
