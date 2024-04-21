using GoalFinder.Data.Repositories.Login;

namespace GoalFinder.Data.UnitOfWork;

/// <summary>
///     Interface for unit of work.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    ///     Gets the login repository.
    /// </summary>
    ILoginRepository LoginRepository { get; }
}
