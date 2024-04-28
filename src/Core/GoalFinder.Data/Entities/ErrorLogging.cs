using System;
using GoalFinder.Data.Entities.Base;

namespace GoalFinder.Data.Entities;

/// <summary>
///     Represent the "ErrorLoggings" table.
/// </summary>
public class ErrorLogging : IBaseEntity
{
    public Guid Id { get; set; }

    public string ErrorMessage { get; set; }

    public string ErrorStackTrace { get; set; }

    public DateTime CreatedAt { get; set; }

    public string Data { get; set; }

    public static class MetaData
    {
        public static class ErrorMessage
        {
            public const int MinLength = 2;
        }

        public static class ErrorStackTrace
        {
            public const int MinLength = 2;
        }

        public static class Data
        {
            public const int MinLength = 2;
        }
    }
}
