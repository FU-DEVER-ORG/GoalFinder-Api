using GoalFinder.Application.Features.Auth.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoalFinder.Application.Features.Auth.ForgotPassword;

public static class ExtensionMethod
{
    public static string ToAppCode(this ForgotPasswordReponseStatusCode statusCode)
    {
        return $"{nameof(Auth)}.{nameof(ForgotPassword)}.{statusCode}";
    }
}
