using System;

namespace GoalFinder.Data.Entities.Base;

/// <summary>
///     Use in case the entity requires the information
///     about the person creating the entity and the time
///     is it created.
/// </summary>
internal interface ICreatedEntity
{
    /// <summary>
    ///     When is entity created.
    /// </summary>
    DateTime CreatedAt { get; set; }

    /// <summary>
    ///     Id of the entity creator.
    /// </summary>
    Guid CreatedBy { get; set; }
}
