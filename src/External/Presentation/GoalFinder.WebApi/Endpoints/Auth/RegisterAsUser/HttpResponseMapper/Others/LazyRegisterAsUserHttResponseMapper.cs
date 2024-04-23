namespace GoalFinder.WebApi.Endpoints.Auth.RegisterAsUser.HttpResponseMapper.Others;

/// <summary>
///     register as user extension methods.
/// </summary>
internal static class LazyRegisterAsUserHttResponseMapper
{
    private static RegisterAsUserHttpResponseManager _registerAsUserHttpResponseManager;

    internal static RegisterAsUserHttpResponseManager Get()
    {
        return _registerAsUserHttpResponseManager ??= new();
    }
}
