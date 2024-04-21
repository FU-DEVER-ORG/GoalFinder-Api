using FastEndpoints.Security;
using GoalFinder.Configuration.Presentation.WebApi.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;

namespace GoalFinder.WebApi.Shared.ServiceConfigs;

/// <summary>
///     Authentication service config.
/// </summary>
internal static class AuthenticationServiceConfig
{
    internal static void ConfigAuthentication(
        this IServiceCollection services,
        IConfigurationManager configuration)
    {
        var option = configuration
            .GetRequiredSection(key: "Authentication")
            .Get<JwtAuthenticationOption>();    

        services.AddAuthenticationJwtBearer(
            jwtSigningOption => jwtSigningOption.SigningKey = option.Jwt.IssuerSigningKey,
            jwtBearerOption =>
            {
                jwtBearerOption.TokenValidationParameters = new()
                {
                    ValidateIssuer = option.Jwt.ValidateIssuer,
                    ValidateAudience = option.Jwt.ValidateAudience,
                    ValidateLifetime = option.Jwt.ValidateLifetime,
                    ValidateIssuerSigningKey = option.Jwt.ValidateIssuerSigningKey,
                    RequireExpirationTime = option.Jwt.RequireExpirationTime,
                    ValidTypes = option.Jwt.ValidTypes,
                    ValidIssuer = option.Jwt.ValidIssuer,
                    ValidAudience = option.Jwt.ValidAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        key: new HMACSHA256(
                            key: Encoding.UTF8.GetBytes(
                                s: option.Jwt.IssuerSigningKey))
                        .Key)
                };

                jwtBearerOption.Validate();
            });
    }
}
