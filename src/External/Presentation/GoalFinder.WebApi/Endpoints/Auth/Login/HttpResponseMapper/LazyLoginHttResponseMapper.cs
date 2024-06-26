﻿namespace GoalFinder.WebApi.Endpoints.Auth.Login.HttpResponseMapper;

/// <summary>
///     login extension methods.
/// </summary>
internal static class LazyLoginHttResponseMapper
{
    private static LoginHttpResponseManager _loginHttpResponseManager;

    internal static LoginHttpResponseManager Get()
    {
        return _loginHttpResponseManager ??= new();
    }
}
