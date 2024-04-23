﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GoalFinder.Data.Repositories.ForgotPassword;

/// <summary>
/// Interface for ForgotPasswordRepository
/// </summary>
public partial interface IForgotPasswordRepository
{
    Task<bool> IsUserTemporarilyRemovedQueryAsync(
        Guid userId,
        CancellationToken cancellationToken
    );
}

