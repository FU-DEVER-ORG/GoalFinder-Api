using System.Text.Json;

namespace GoalFinder.RedisCachingDb.Common;

internal static class CommonObject
{
    internal static readonly JsonSerializerOptions Option = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

}
