﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoalFinder.Application.Features.Auth.ForgotPassword;
/// <summary>
/// Forgot Password Response Status Code
/// </summary>
public enum ForgotPasswordReponseStatusCode
{
    USER_WITH_EMAIL_IS_NOT_FOUND,
    USER_IS_TEMPORARILY_REMOVED,
    USER_IS_NOT_VERIFY,
    INPUT_VALIDATION_FAIL,
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL
}
