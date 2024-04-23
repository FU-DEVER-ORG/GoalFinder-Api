using GoalFinder.Data.Repositories.InsertErrorLog;
using GoalFinder.Data.Repositories.Login;
using GoalFinder.Data.Repositories.RegisterAsUser;

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

    /// <summary>
    ///     Gets the insert error log repository.
    /// </summary>
    IInsertErrorLogRepository InsertErrorLogRepository { get; }

    /// <summary>
    ///     Gets the register as user repository.
    /// </summary>
    IRegisterAsUserRepository RegisterAsUserRepository { get; }
}
