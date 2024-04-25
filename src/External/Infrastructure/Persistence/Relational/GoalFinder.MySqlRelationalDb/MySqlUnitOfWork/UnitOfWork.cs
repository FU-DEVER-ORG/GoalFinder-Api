using GoalFinder.Data.Repositories.ForgotPassword;
using GoalFinder.Data.Repositories.GetUserProfile;
using GoalFinder.Data.Repositories.InsertErrorLog;
using GoalFinder.Data.Repositories.Login;
using GoalFinder.Data.Repositories.RegisterAsUser;
using GoalFinder.Data.Repositories.ResetPasswordWithOtp;
using GoalFinder.Data.Repositories.UpdateUserInfo;
using GoalFinder.Data.UnitOfWork;
using GoalFinder.MySqlRelationalDb.Data;
using GoalFinder.MySqlRelationalDb.Repositories.ForgotPassword;
using GoalFinder.MySqlRelationalDb.Repositories.GetUserProfile;
using GoalFinder.MySqlRelationalDb.Repositories.InsertErrorLog;
using GoalFinder.MySqlRelationalDb.Repositories.Login;
using GoalFinder.MySqlRelationalDb.Repositories.RegisterAsUser;
using GoalFinder.MySqlRelationalDb.Repositories.UpdateUserInfo;
using GoalFinder.MySqlRelationalDb.Repositories.ResetPasswordWithOtp;

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
    private IResetPasswordWithOtpRepository _resetPasswordWithOtpRepository;

    private IGetUserProfileRepository _getUserProfileRepository;

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

    public IGetUserProfileRepository GetUserProfileRepository
    {
        get
        {
            _getUserProfileRepository ??= new GetUserProfileRepository(context: _context);

            return _getUserProfileRepository;
        }
    }

    public IResetPasswordWithOtpRepository ResetPasswordWithOtpRepository
    {
        get
        {
            _resetPasswordWithOtpRepository ??= new ResetPasswordWithOtpRepository(context: _context);

            return _resetPasswordWithOtpRepository;
        }
    }
}
