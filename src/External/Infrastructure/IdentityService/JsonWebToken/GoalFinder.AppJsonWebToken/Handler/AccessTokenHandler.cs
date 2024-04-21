using FastEndpoints.Security;
using GoalFinder.Application.Shared.Tokens.AccessToken;
using GoalFinder.Configuration.Presentation.WebApi.Authentication;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace GoalFinder.AppJsonWebToken.Handler;

/// <summary>
///     Implementation of jwt generator interface.
/// </summary>
internal sealed class AccessTokenHandler : IAccessTokenHandler
{
    private readonly JwtAuthenticationOption _jwtAuthenticationOption;

    public AccessTokenHandler(JwtAuthenticationOption jwtAuthenticationOption)
    {
        _jwtAuthenticationOption = jwtAuthenticationOption;
    }

    public string GenerateSigningToken(IEnumerable<Claim> claims)
    {
        // Validate set of user claims.
        if (claims.Equals(obj: Enumerable.Empty<Claim>()) ||
            Equals(objA: claims, objB: default))
        {
            return string.Empty;
        }

        return JwtBearer.CreateToken(options: option =>
        {
            option.SigningKey = _jwtAuthenticationOption.Jwt.IssuerSigningKey;
            option.ExpireAt = DateTime.UtcNow.AddMinutes(value: 15);
            option.User.Claims.AddRange(collection: claims);
            option.Audience = _jwtAuthenticationOption.Jwt.ValidAudience;
            option.Issuer = _jwtAuthenticationOption.Jwt.ValidIssuer;
            option.SymmetricKeyAlgorithm = SecurityAlgorithms.HmacSha256;
            option.CompressionAlgorithm = CompressionAlgorithms.Deflate;
            option.User.Claims.Add(item: new(
                type: JwtRegisteredClaimNames.Iat,
                value: DateTime.UtcNow.ToLongTimeString()));
        });
    }
}
