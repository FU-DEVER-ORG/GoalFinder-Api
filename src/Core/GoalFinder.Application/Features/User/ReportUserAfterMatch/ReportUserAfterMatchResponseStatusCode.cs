namespace GoalFinder.Application.Features.User.ReportUserAfterMatch;

/// <summary>
///      Report user after match response status code
/// </summary>

public enum ReportUserAfterMatchResponseStatusCode
{
    OPERATION_SUCCESS,
    USER_IS_NOT_FOUND,
    FOOTBALL_MATCH_IS_NOT_FOUND,
    DATABASE_OPERATION_FAIL,
    USER_IS_TEMPORARILY_REMOVED,
    FOOTBALL_MATCH_ENDTIME_IS_STILL_AVAILABLE,
    PLAYER_DOES_NOT_EXIST_IN_FOOTBALL_MATCH,
    FORBIDDEN
}
