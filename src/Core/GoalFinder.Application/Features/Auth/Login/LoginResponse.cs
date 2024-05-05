using GoalFinder.Application.Shared.Features;

namespace GoalFinder.Application.Features.Auth.Login;

/// <summary>
///     Login response.
/// </summary>
public sealed class LoginResponse : IFeatureResponse
{
    public LoginResponseStatusCode StatusCode { get; init; }

    public Body ResponseBody { get; init; }

    public sealed class Body
    {
        public string AccessToken { get; init; }

        public string RefreshToken { get; init; }

        public UserCredential User { get; init; }

        public sealed class UserCredential
        {
            public string Email { get; init; }

            public string AvatarUrl { get; init; }

            public string FirstName { get; init; }

            public string LastName { get; init; }

            public string UserName { get; init; }
        }
    }
}
