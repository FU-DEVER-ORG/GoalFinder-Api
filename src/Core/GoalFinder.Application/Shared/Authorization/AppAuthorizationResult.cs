namespace GoalFinder.Application.Shared.Authorization;

/// <summary>
///     Represents the result of an application authorization.
/// </summary>
public sealed class AppAuthorizationResult
{
    /// <summary>
    ///     Gets a value indicating whether the feature is authorized.
    /// </summary>
    public bool IsAuthorized { get; }

    /// <summary>
    ///     Gets a value indicating whether the feature is forbidden.
    /// </summary>
    public bool IsForbidden { get; }

    private AppAuthorizationResult(bool isAuthorized, bool isForbidden)
    {
        IsAuthorized = isAuthorized;
        IsForbidden = isForbidden;
    }

    /// <summary>
    ///     Create application forbidden result.
    /// </summary>
    /// <returns>
    ///     App authorization result.
    /// </returns>
    public static AppAuthorizationResult Forbidden()
    {
        return new(isAuthorized: false, isForbidden: true);
    }

    /// <summary>
    ///     Create application unauthorized result.
    /// </summary>
    /// <returns>
    ///     App authorization result.
    /// </returns>
    public static AppAuthorizationResult UnAuthorized()
    {
        return new(isAuthorized: false, isForbidden: false);
    }

    /// <summary>
    ///     Create application authorized result.
    /// </summary>
    /// <returns>
    ///     App authorization result.
    /// </returns>
    public static AppAuthorizationResult Authorized()
    {
        return new(isAuthorized: true, isForbidden: false);
    }
}
