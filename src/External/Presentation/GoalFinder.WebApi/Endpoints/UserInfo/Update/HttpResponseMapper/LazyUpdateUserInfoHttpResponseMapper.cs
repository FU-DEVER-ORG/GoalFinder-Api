namespace GoalFinder.WebApi.Endpoints.UserInfo.Update.HttpResponseMapper
{
    internal static class LazyUpdateUserInfoHttpResponseMapper
    {
        private static UpdateUserInfoHttpResponseManager _updateUserInfoHttpResponseManager;
        internal static UpdateUserInfoHttpResponseManager Get()
        {
            return _updateUserInfoHttpResponseManager ??= new();
        }
    }
}
