namespace GoalFinder.Application.Features.UserInfo.Update
{
    public enum UpdateUserInfoResponseStatusCode
    {
        UPDATE_SUCCESS,
        USER_NOT_FOUND,
        DATABASE_OPERATION_FAIL,
        USERNAME_IS_EXISTED,
        INPUT_VALIDATION_FAIL
    }
}
