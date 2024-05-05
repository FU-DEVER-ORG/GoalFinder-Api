﻿namespace GoalFinder.Application.Features.Match.GetMatchDetail;

public enum GetMatchDetailResponseStatusCode
{
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAILED,
    USER_IS_TEMPORARILY_REMOVED,
    UN_AUTHORIZED,
    FORBIDDEN,
    INPUT_NOT_UNDERSTANDABLE,
    INPUT_VALIDATION_FAIL,
    FOOTBALL_MATCH_NOT_FOUND
}
