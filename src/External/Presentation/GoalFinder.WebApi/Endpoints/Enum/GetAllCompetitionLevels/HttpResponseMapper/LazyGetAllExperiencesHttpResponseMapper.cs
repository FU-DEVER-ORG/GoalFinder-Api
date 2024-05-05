namespace GoalFinder.WebApi.Endpoints.Enum.GetAllCompetitionLevels.HttpResponseMapper;

internal static class LazyGetAllCompetitionLevelsHttpResponseMapper
{
    private static GetAllCompetitionLevelsHttpResponseManager _getAllCompetitionLevelsHttpResponseManager;

    internal static GetAllCompetitionLevelsHttpResponseManager Get()
    {
        return _getAllCompetitionLevelsHttpResponseManager ??= new();
    }
}
