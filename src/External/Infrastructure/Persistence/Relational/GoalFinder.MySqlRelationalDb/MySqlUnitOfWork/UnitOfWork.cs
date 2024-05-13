using GoalFinder.Data.Repositories.CreateMatch;
using GoalFinder.Data.Repositories.ForgotPassword;
using GoalFinder.Data.Repositories.GetAllCompetitionLevels;
using GoalFinder.Data.Repositories.GetAllExperiences;
using GoalFinder.Data.Repositories.GetAllMatches;
using GoalFinder.Data.Repositories.GetMatchesUpcoming;
using GoalFinder.Data.Repositories.GetAllPositions;
using GoalFinder.Data.Repositories.GetMatchDetailRepository;
using GoalFinder.Data.Repositories.GetReportNotification;
using GoalFinder.Data.Repositories.GetUserInfoOnSidebar;
using GoalFinder.Data.Repositories.GetUserProfile;
using GoalFinder.Data.Repositories.GetUserProfileByUserId;
using GoalFinder.Data.Repositories.InsertErrorLog;
using GoalFinder.Data.Repositories.Login;
using GoalFinder.Data.Repositories.RefreshAccessTokenRepository;
using GoalFinder.Data.Repositories.RegisterAsUser;
using GoalFinder.Data.Repositories.ResetPasswordWithOtp;
using GoalFinder.Data.Repositories.UpdateUserInfo;
using GoalFinder.Data.UnitOfWork;
using GoalFinder.MySqlRelationalDb.Data;
using GoalFinder.MySqlRelationalDb.Repositories.CreateMatch;
using GoalFinder.MySqlRelationalDb.Repositories.ForgotPassword;
using GoalFinder.MySqlRelationalDb.Repositories.GetAllCompetitionLevels;
using GoalFinder.MySqlRelationalDb.Repositories.GetAllExperiences;
using GoalFinder.MySqlRelationalDb.Repositories.GetAllMatches;
using GoalFinder.MySqlRelationalDb.Repositories.GetMatchesUpcoming;
using GoalFinder.MySqlRelationalDb.Repositories.GetAllPositions;
using GoalFinder.MySqlRelationalDb.Repositories.GetMatchDetailRepository;
using GoalFinder.MySqlRelationalDb.Repositories.GetReportNotification;
using GoalFinder.MySqlRelationalDb.Repositories.GetUserInfoOnSidebar;
using GoalFinder.MySqlRelationalDb.Repositories.GetUserProfile;
using GoalFinder.MySqlRelationalDb.Repositories.GetUserProfileByUserId;
using GoalFinder.MySqlRelationalDb.Repositories.InsertErrorLog;
using GoalFinder.MySqlRelationalDb.Repositories.Login;
using GoalFinder.MySqlRelationalDb.Repositories.RefreshAccessTokenRepository;
using GoalFinder.MySqlRelationalDb.Repositories.RegisterAsUser;
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
    private IGetMatchesUpcomingRepository _getMatchesUpcomingRepository;
    private IGetAllPositionsRepository _getAllPositionsRepository;
    private IGetAllCompetitionLevelsRepository _getAllCompetitionLevelsRepository;
    private IGetAllExperiencesRepository _getAllExperiencesRepository;
    private IGetMatchDetailRepository _getMatchDetailRepository;
    private IGetUserProfileByUserIdRepository _getUserProfileByUserIdRepository;
    private IGetReportNotificationRepository _getReportNotificationRepository;

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

    public IGetMatchesUpcomingRepository GetMatchesUpcomingRepository
    {
        get
        {
            _getMatchesUpcomingRepository ??= new GetMatchesUpcomingRepository(context: _context);
            return _getMatchesUpcomingRepository;
        }
    }

    public IGetAllPositionsRepository GetAllPositionsRepository
    {
        get
        {
            _getAllPositionsRepository ??= new GetAllPositionsRepository(context: _context);

            return _getAllPositionsRepository;
        }
    }

    public IGetAllCompetitionLevelsRepository GetAllCompetitionLevelsRepository
    {
        get
        {
            _getAllCompetitionLevelsRepository ??= new GetAllCompetitionLevelsRepository(
                context: _context
            );
            return _getAllCompetitionLevelsRepository;
        }
    }

    public IGetAllExperiencesRepository GetAllExperiencesRepository
    {
        get
        {
            _getAllExperiencesRepository ??= new GetAllExperiencesRepository(context: _context);
            return _getAllExperiencesRepository;
        }
    }
    public IGetMatchDetailRepository GetMatchDetailRepository
    {
        get
        {
            _getMatchDetailRepository ??= new GetMatchDetailRepository(context: _context);

            return _getMatchDetailRepository;
        }
    }

    public IGetUserProfileByUserIdRepository GetUserProfileByUserIdRepository
    {
        get
        {
            _getUserProfileByUserIdRepository ??= new GetUserProfileByUserIdRepository(
                context: _context
            );
            return _getUserProfileByUserIdRepository;
        }
    }

    public IGetReportNotificationRepository GetReportNotificationRepository
    {
        get
        {
            _getReportNotificationRepository ??= new GetReportNotificationRepository(
                context: _context
            );

            return _getReportNotificationRepository;
        }
    }
}
