using GoalFinder.Application.Features.Auth.RefreshAccessToken;
using System;
using System.Collections.Generic;

namespace GoalFinder.WebApi.Endpoints.Auth.RefreshAccessToken.HttpResponseMapper;

internal sealed class RefreshAccessTokenHttpResponseManager
{
    private readonly Dictionary<
        RefreshAccessTokenResponseStatusCode,
        Func<
            RefreshAccessTokenRequest,
            RefreshAccessTokenResponse,
            RefreshAccessTokenHttpResponse
            >>
            _dictionary;
    internal RefreshAccessTokenHttpResponseManager()
    {
        _dictionary = [];
    }
    internal Func<
        RefreshAccessTokenRequest,
        RefreshAccessTokenResponse,
        RefreshAccessTokenHttpResponse>
            Resolve(RefreshAccessTokenResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
