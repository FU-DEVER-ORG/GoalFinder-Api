namespace GoalFinder.WebApi.Endpoints.User.UserInfoUpdate.HttpResponseMapper;

internal static class LazyUpdateUserInfoHttpResponseMapper
{
    private static UpdateUserInfoHttpResponseManager _updateUserInfoHttpResponseManager;

    internal static UpdateUserInfoHttpResponseManager Get()
    {
        return _updateUserInfoHttpResponseManager ??= new();
    }
}
