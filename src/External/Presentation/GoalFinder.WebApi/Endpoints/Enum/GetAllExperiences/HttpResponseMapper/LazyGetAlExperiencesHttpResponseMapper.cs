namespace GoalFinder.WebApi.Endpoints.Enum.GetAllExperiences.HttpResponseMapper;

internal static class LazyGetAllExperiencesHttpResponseMapper
{
    private static GetAllExperiencesHttpResponseManager _getAllExperiencesHttpResponseManager;

    internal static GetAllExperiencesHttpResponseManager Get()
    {
        return _getAllExperiencesHttpResponseManager ??= new();
    }
}
