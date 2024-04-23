using GoalFinder.Data.Repositories.ForgotPassword;
using GoalFinder.Data.Repositories.InsertErrorLog;
using GoalFinder.Data.Repositories.Login;
using GoalFinder.Data.Repositories.RegisterAsUser;
using GoalFinder.Data.Repositories.UpdateUserInfo;
using GoalFinder.Data.UnitOfWork;
using GoalFinder.MySqlRelationalDb.Data;
using GoalFinder.MySqlRelationalDb.Repositories.ForgotPassword;
using GoalFinder.MySqlRelationalDb.Repositories.InsertErrorLog;
using GoalFinder.MySqlRelationalDb.Repositories.Login;
using GoalFinder.MySqlRelationalDb.Repositories.RegisterAsUser;
using GoalFinder.MySqlRelationalDb.Repositories.UpdateUserInfo;

namespace GoalFinder.MySqlRelationalDb.MySqlUnitOfWork;

/// <summary>
///     Implementation of unit of work interface.
/// </summary>
internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly GoalFinderContext _context;
    private ILoginRepository _loginRepository;
    private IInsertErrorLogRepository _insertErrorLogRepository;
    private IForgotPasswordRepository _forgotPasswordRepository;
    private IRegisterAsUserRepository _registerAsUserRepository;
    private IUpdateUserInfoRepository _updateUserInfoRepository;

    public UnitOfWork(GoalFinderContext context)
    {
        _context = context;
    }

    public ILoginRepository LoginRepository
    {
        get
        {
            _loginRepository ??= new LoginRepository(context: _context);

            return _loginRepository;
        }
    }

    public IInsertErrorLogRepository InsertErrorLogRepository
    {
        get
        {
            _insertErrorLogRepository ??= new InsertErrorLogRepository(context: _context);

            return _insertErrorLogRepository;
        }
    }

    public IForgotPasswordRepository ForgotPasswordRepository
    {
        get
        {
            _forgotPasswordRepository ??= new ForgotPasswordRepository(context: _context);
            return _forgotPasswordRepository;
        }
    }
    public IRegisterAsUserRepository RegisterAsUserRepository
    {
        get
        {
            _registerAsUserRepository ??= new RegisterAsUserRepository(context: _context);

            return _registerAsUserRepository;
        }
    }

    public IUpdateUserInfoRepository UpdateUserInfoRepository
    {
        get
        {
            _updateUserInfoRepository ??= new UpdateUserInfoRepository(context: _context);

            return _updateUserInfoRepository;
        }
    }
}
