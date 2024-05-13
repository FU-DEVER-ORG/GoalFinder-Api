namespace GoalFinder.Application.Features.Match.GetMatchesUpcoming;

/// <summary>
///     Feature response status code.
/// </summary>
public enum GetMatchesUpcomingResponseStatusCode
{
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
    FORBIDDEN,
    USER_IS_NOT_FOUND,
    USER_IS_TEMPORARILY_REMOVED,
}
