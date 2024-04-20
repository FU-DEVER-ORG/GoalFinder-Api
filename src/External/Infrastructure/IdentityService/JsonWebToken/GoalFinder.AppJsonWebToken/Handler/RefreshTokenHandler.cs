using GoalFinder.Application.Shared.Tokens.RefreshToken;
using GoalFinder.Data.Entities;
using System.Security.Cryptography;
using System.Text;

namespace GoalFinder.AppJsonWebToken.Handler;

/// <summary>
///     Implementation refresh token generator interface.
/// </summary>
internal sealed class RefreshTokenHandler : IRefreshTokenHandler
{
    public string Generate(int length)
    {
        const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890abcdefghijklmnopqrstuvwxyz!@#$%^&*+=";

        if (length < RefreshToken.MetaData.RefreshTokenValue.MinLength)
        {
            return string.Empty;
        }

        StringBuilder builder = new();

        for (int time = default; time < length; time++)
        {
            builder.Append(
                value: Chars[index: RandomNumberGenerator.GetInt32(
                    toExclusive: Chars.Length)]);
        }

        return builder.ToString();
    }
}
