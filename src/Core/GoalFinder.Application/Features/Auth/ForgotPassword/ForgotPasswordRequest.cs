﻿using GoalFinder.Application.Shared.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoalFinder.Application.Features.Auth.ForgotPassword;

public sealed class ForgotPasswordRequest : IFeatureRequest<ForgotPasswordResponse>
{
    public string UserName { get; init; }
}