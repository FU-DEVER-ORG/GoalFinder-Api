using System.Security.Cryptography;
using System.Text;
using GoalFinder.Application.Shared.Tokens.OTP;

namespace GoalFinder.AppOTP.Handler;

internal sealed class OtpGenerator : IOtpHandler
{
    public string Generate(int length)
    {
        const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

        StringBuilder builder = new();

        for (int time = default; time < length; time++)
        {
            builder.Append(
                value: Chars[index: RandomNumberGenerator.GetInt32(toExclusive: Chars.Length)]
            );
        }

        return builder.ToString();
    }
}
