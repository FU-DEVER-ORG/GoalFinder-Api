﻿using GoalFinder.Data.Repositories.Login;
using GoalFinder.Data.UnitOfWork;
using GoalFinder.MySqlRelationalDb.Data;
using GoalFinder.MySqlRelationalDb.Repositories.Login;

namespace GoalFinder.MySqlRelationalDb.MySqlUnitOfWork;

/// <summary>
///     Implementation of unit of work interface.
/// </summary>
internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly GoalFinderContext _context;
    private ILoginRepository _loginRepository;

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
}