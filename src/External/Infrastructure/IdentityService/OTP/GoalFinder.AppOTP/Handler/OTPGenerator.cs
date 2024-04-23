using GoalFinder.Application.Shared.Tokens.OTP;
using GoalFinder.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GoalFinder.AppOTP.Handler;

internal class OTPGenerator : IOtpHandler
{
    public string Generate(int length)
    {
        const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

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
