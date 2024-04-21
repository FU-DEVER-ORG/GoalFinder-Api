using System.Collections.Generic;
using System.Security.Claims;

namespace GoalFinder.Application.Shared.Tokens.AccessToken;

/// <summary>
///     Represent jwt generator interface.
/// </summary>
public interface IAccessTokenHandler
{
    /// <summary>
    ///     Generate jwt base on list of claims.
    /// </summary>
    /// <param name="claims">
    ///     List of user claims.
    /// </param>
    /// <returns>
    ///     A string having format of jwt
    ///     or empty string if validate fail.
    /// </returns>
    string GenerateSigningToken(IEnumerable<Claim> claims);
}
