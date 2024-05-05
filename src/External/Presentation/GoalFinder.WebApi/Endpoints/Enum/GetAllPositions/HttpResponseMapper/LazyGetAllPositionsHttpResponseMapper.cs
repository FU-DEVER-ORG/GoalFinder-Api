namespace GoalFinder.WebApi.Endpoints.Enum.GetAllPositions.HttpResponseMapper;

internal static class LazyGetAllPositionsHttpResponseMapper
{
    private static GetAllPositionsHttpResponseManager _getAllPositionHttpResponseManager;

    internal static GetAllPositionsHttpResponseManager Get()
    {
        return _getAllPositionHttpResponseManager ??= new();
    }
}
