using System;

namespace GoalFinder.Application.Shared.Commons;

/// <summary>
///     Represent set of constant.
/// </summary>
public static class CommonConstant
{
    public static object Database { get; set; }

    public static class App
    {
        public static readonly Guid DEFAULT_ENTITY_ID_AS_GUID = Guid.Parse(input: "aa403b53-5ae7-4e50-8b02-92dce06ed1a9");
    }
}
