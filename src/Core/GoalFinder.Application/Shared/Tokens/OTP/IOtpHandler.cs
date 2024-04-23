using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoalFinder.Application.Shared.Tokens.OTP;
/// <summary>
/// Interface for Otp Handler
/// </summary>
public interface IOtpHandler
{
    /// <summary>
    ///  Generate Otp
    /// </summary>
    /// <param name="length"></param>
    /// <returns></returns>
    string Generate(int length);
}
