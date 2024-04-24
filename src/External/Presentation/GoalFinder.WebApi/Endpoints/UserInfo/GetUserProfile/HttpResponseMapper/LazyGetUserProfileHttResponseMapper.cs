namespace GoalFinder.WebApi.Endpoints.UserInfo.GetUserProfile.HttpResponseMapper;

/// <summary>
///     GetUserProfile extension methods.
/// </summary>
internal static class LazyGetUserProfileHttResponseMapper
{
    private static GetUserProfileHttpResponseManager _GetUserProfileHttpResponseManager;

    internal static GetUserProfileHttpResponseManager Get()
    {
        return _GetUserProfileHttpResponseManager ??= new();
    }
}
