namespace GoalFinder.WebApi.Endpoints.UserInfo.GetUserProfileByUserId.HttpResponseMapper;

/// <summary>
/// Lazy Get user profile by user id http response manager
/// </summary>

internal static class LazyGetUserProfileByUserIdHttpResponseMapper
{
    private static GetUserProfileByUserIdHttpResponseManager _GetUserProfileByUserIdHttpResponseManager;

    internal static GetUserProfileByUserIdHttpResponseManager Get()
    {
        return _GetUserProfileByUserIdHttpResponseManager ??= new();
    }
}
