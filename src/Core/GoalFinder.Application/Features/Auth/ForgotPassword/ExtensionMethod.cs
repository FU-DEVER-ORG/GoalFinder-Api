using GoalFinder.Application.Features.Auth.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoalFinder.Application.Features.Auth.ForgotPassword;

/// <summary>
/// Extension Method
/// </summary>
public static class ExtensionMethod
{
    /// <summary>
    /// ToAppCode
    /// </summary>
    /// <param name="statusCode"></param>
    /// <returns></returns>
    public static string ToAppCode(this ForgotPasswordReponseStatusCode statusCode)
    {
        return $"{nameof(Auth)}.{nameof(ForgotPassword)}.{statusCode}";
    }
}
