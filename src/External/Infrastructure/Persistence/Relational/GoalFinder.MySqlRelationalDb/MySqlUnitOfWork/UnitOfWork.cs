using GoalFinder.Data.Repositories.CreateMatch;
using GoalFinder.Data.Repositories.ForgotPassword;
using GoalFinder.Data.Repositories.GetAllMatches;
using GoalFinder.Data.Repositories.GetAllReports;
using GoalFinder.Data.Repositories.GetUserInfoOnSidebar;
using GoalFinder.Data.Repositories.GetUserProfile;
using GoalFinder.Data.Repositories.InsertErrorLog;
using GoalFinder.Data.Repositories.Login;
using GoalFinder.Data.Repositories.RefreshAccessTokenRepository;
using GoalFinder.Data.Repositories.RegisterAsUser;
using GoalFinder.Data.Repositories.ReportUserAfterMatch;
using GoalFinder.Data.Repositories.ResetPasswordWithOtp;
using GoalFinder.Data.Repositories.UpdateUserInfo;
using GoalFinder.Data.UnitOfWork;
using GoalFinder.MySqlRelationalDb.Data;
using GoalFinder.MySqlRelationalDb.Repositories.CreateMatch;
using GoalFinder.MySqlRelationalDb.Repositories.ForgotPassword;
using GoalFinder.MySqlRelationalDb.Repositories.GetAllMatches;
using GoalFinder.MySqlRelationalDb.Repositories.GetAllReports;
using GoalFinder.MySqlRelationalDb.Repositories.GetUserInfoOnSidebar;
using GoalFinder.MySqlRelationalDb.Repositories.GetUserProfile;
using GoalFinder.MySqlRelationalDb.Repositories.InsertErrorLog;
using GoalFinder.MySqlRelationalDb.Repositories.Login;
using GoalFinder.MySqlRelationalDb.Repositories.RefreshAccessTokenRepository;
using GoalFinder.MySqlRelationalDb.Repositories.RegisterAsUser;
using GoalFinder.MySqlRelationalDb.Repositories.ReportUserAfterMatch;
using GoalFinder.MySqlRelationalDb.Repositories.ResetPasswordWithOtp;
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
    private IResetPasswordWithOtpRepository _resetPasswordWithOtpRepository;
    private IGetUserProfileRepository _getUserProfileRepository;
    private IGetAllMatchesRepository _getAllMatchesRepository;
    private IRefreshAccessTokenRepository _refreshAccessTokenRepository;
    private IGetUserInfoOnSidebarRepository _getUserInfoOnSidebarRepository;
    private ICreateMatchRepository _createMatchRepository;
    private IReportUserAfterMatchRepository _reportUserAfterMatchRepository;
    private IGetAllReportsRepository _getAllReportsRepository;

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

    public IGetAllMatchesRepository GetAllMatchesRepository
    {
        get
        {
            _getAllMatchesRepository ??= new GetAllMatchesRepository(context: _context);
            return _getAllMatchesRepository;
        }
    }
    public IResetPasswordWithOtpRepository ResetPasswordWithOtpRepository
    {
        get
        {
            _resetPasswordWithOtpRepository ??= new ResetPasswordWithOtpRepository(
                context: _context
            );

            return _resetPasswordWithOtpRepository;
        }
    }

    public IRefreshAccessTokenRepository RefreshAccessTokenRepository
    {
        get
        {
            _refreshAccessTokenRepository ??= new RefreshAccessTokenRepository(context: _context);
            return _refreshAccessTokenRepository;
        }
    }
    public IGetUserInfoOnSidebarRepository GetUserInfoOnSidebarRepository
    {
        get
        {
            _getUserInfoOnSidebarRepository ??= new GetUserInfoOnSidebarRepository(
                context: _context
            );

            return _getUserInfoOnSidebarRepository;
        }
    }

    public ICreateMatchRepository CreateMatchRepository
    {
        get
        {
            _createMatchRepository ??= new CreateMatchRepository(context: _context);

            return _createMatchRepository;
        }
    }

    public IReportUserAfterMatchRepository ReportUserAfterMatchRepository
    {
        get
        {
            _reportUserAfterMatchRepository ??= new ReportUserAfterMatchRepository(
                context: _context
            );

            return _reportUserAfterMatchRepository;
        }
    }

    public IGetAllReportsRepository GetAllReportsRepository
    {
        get
        {
            _getAllReportsRepository ??= new GetAllReportsRepository(
                context: _context
            );

            return _getAllReportsRepository;    
        }
    }
}
