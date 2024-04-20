using GoalFinder.Data.UnitOfWork;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;
using System.Threading;
using GoalFinder.MySqlRelationalDb.Data;
using GoalFinder.Data.Repositories.RefreshToken;
using GoalFinder.MySqlRelationalDb.Repositories.RefreshToken;
using GoalFinder.Data.Repositories.UserDetail;
using GoalFinder.MySqlRelationalDb.Repositories.UserDetail;

namespace GoalFinder.MySqlRelationalDb.MySqlUnitOfWork;

/// <summary>
///     Implementation of unit of work interface.
/// </summary>
internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly GoalFinderContext _context;
    private IDbContextTransaction _dbTransaction;
    private IRefreshTokenRepository _refreshTokenRepository;
    private IUserDetailRepository _userDetailRepository;

    public UnitOfWork(GoalFinderContext context)
    {
        _context = context;
    }

    public IRefreshTokenRepository RefreshTokenRepository
    {
        get
        {
            _refreshTokenRepository ??= new RefreshTokenRepository(context: _context);

            return _refreshTokenRepository;
        }
    }

    public IUserDetailRepository UserDetailRepository
    {
        get
        {
            _userDetailRepository ??= new UserDetailRepository(context: _context);

            return _userDetailRepository;
        }
    }

    public async Task CreateTransactionAsync(CancellationToken cancellationToken)
    {
        _dbTransaction = await _context.Database.BeginTransactionAsync(cancellationToken: cancellationToken);
    }

    public Task CommitTransactionAsync(CancellationToken cancellationToken)
    {
        return _dbTransaction.CommitAsync(cancellationToken: cancellationToken);
    }

    public Task RollBackTransactionAsync(CancellationToken cancellationToken)
    {
        return _dbTransaction.RollbackAsync(cancellationToken: cancellationToken);
    }

    public ValueTask DisposeTransactionAsync()
    {
        return _dbTransaction.DisposeAsync();
    }

    public IExecutionStrategy CreateExecutionStrategy()
    {
        return _context.Database.CreateExecutionStrategy();
    }
    public Task SaveToDatabaseAsync(CancellationToken cancellationToken)
    {
        return _context.SaveChangesAsync(cancellationToken: cancellationToken);
    }
}
