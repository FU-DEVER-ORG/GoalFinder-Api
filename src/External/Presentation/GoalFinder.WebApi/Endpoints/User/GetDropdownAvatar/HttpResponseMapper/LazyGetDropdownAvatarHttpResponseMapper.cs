namespace GoalFinder.WebApi.Endpoints.User.GetDropdownAvatar.HttpResponseMapper;

internal static class LazyGetDropdownAvatarHttpResponseMapper
{
    private static GetDropdownAvatarHttpResponseManager _getDropdownAvatarHttpResponseManager;

    internal static GetDropdownAvatarHttpResponseManager Get()
    {
        return _getDropdownAvatarHttpResponseManager ??= new();
    }
}
