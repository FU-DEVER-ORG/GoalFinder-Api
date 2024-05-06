namespace GoalFinder.Application.Features.Match.CreateMatch;

/// <summary>
///     Create match response status code.
/// </summary>
public enum CreateMatchResponseStatusCode
{
    USER_ID_IS_NOT_FOUND,
    USER_IS_TEMPORARILY_REMOVED,
    UN_AUTHORIZED,
    FORBIDDEN,
    INPUT_VALIDATION_FAIL,
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
    INPUT_NOT_UNDERSTANDABLE,
    LIMIT_ONE_MATCH_PER_DAY,
    PRESTIGE_IS_NOT_ENOUGH
}
