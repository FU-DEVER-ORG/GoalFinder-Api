﻿namespace GoalFinder.WebApi.Endpoints.UserInfo.Update.Common
{
    /// <summary>
    /// Represents the state bag used for the update user information flow.
    /// </summary>
    internal sealed class UpdateUserInfoStateBag
    {
        internal string CacheKey { get; set; }

        internal int CacheDurationInSeconds { get; } = 60;
    }
}
