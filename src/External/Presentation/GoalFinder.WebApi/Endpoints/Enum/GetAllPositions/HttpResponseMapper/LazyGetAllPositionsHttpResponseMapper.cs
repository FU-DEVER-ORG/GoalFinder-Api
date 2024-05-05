namespace GoalFinder.WebApi.Endpoints.Enum.GetAllPositions.HttpResponseMapper;

internal static class LazyGetAllPositionsHttpResponseMapper
{
    private static GetAllPositionsHttpResponseManager _updateUserInfoHttpResponseManager;

    internal static GetAllPositionsHttpResponseManager Get()
    {
        return _updateUserInfoHttpResponseManager ??= new();
    }
}
