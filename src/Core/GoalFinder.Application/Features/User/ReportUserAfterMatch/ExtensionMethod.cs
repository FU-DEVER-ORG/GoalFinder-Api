using GoalFinder.Application.Features.User.UpdateUserInfo;

namespace GoalFinder.Application.Features.User.ReportUserAfterMatch;

public static class ExtensionMethod 
{
    /// <summary>
    ///     Mapping from feature response status code to
    ///     app code.
    /// </summary>
    /// <param name="statusCode">
    ///     Feature response status code
    /// </param>
    /// <returns>
    ///     New app code.
    /// </returns>
    public static string ToAppCode(this ReportUserAfterMatchResponseStatusCode statusCode)
    {
        return $"{nameof(User)}.{nameof(ReportUserAfterMatch)}.{statusCode}";
    }

}
