namespace GoalFinder.WebApi.Endpoints.User.GetAllReports.Common;

/// <summary>
///     Represents the GetAllReports state bag.
/// </summary>
public class GetAllReportsStateBag
{
    internal string CacheKey { get; set; }

    internal int CacheDurationInSeconds { get; } = 60;
}
