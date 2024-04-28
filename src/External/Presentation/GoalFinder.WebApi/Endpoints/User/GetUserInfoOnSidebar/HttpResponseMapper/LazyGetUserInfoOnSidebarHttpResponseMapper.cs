namespace GoalFinder.WebApi.Endpoints.User.GetUserInfoOnSidebar.HttpResponseMapper;

internal static class LazyGetUserInfoOnSidebarHttpResponseMapper
{
    private static GetUserInfoOnSidebarHttpResponseManager _updateUserInfoHttpResponseManager;

    internal static GetUserInfoOnSidebarHttpResponseManager Get()
    {
        return _updateUserInfoHttpResponseManager ??= new();
    }
}
