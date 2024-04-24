namespace GoalFinder.WebApi.Endpoints.Auth.RegisterAsUser.Common;

/// <summary>
///     Represents the register as user state bag.
/// </summary>
internal sealed class RegisterAsUserStateBag
{
    internal string CacheKey { get; set; }

    internal int CacheDurationInSeconds { get; } = 60;
}
