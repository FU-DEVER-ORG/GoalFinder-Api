using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoalFinder.Application.Features.UserInfo.Update
{
    public static class ExtensionMethod
    {
        public static String ToAppCode(this UpdateUserInfoResponseStatusCode statusCode)
        {
            return $"{nameof(UserInfo)}.{nameof(Update)}.{statusCode}";
        }

    }
}
