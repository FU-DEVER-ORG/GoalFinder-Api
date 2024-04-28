namespace GoalFinder.Application.Features.Auth.RefreshAccessToken;

public enum RefreshAccessTokenResponseStatusCode
{
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
    UN_AUTHORIZED,
    INPUT_VALIDATION_FAIL,
    REFRESH_TOKEN_IS_NOT_FOUND,
    REFRESH_TOKEN_IS_EXPIRED,
    FORBIDDEN
}
