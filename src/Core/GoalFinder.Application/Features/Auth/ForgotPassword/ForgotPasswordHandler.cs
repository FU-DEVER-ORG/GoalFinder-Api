using GoalFinder.Application.Shared.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GoalFinder.Application.Features.Auth.ForgotPassword
{
    internal class ForgotPasswordHandler : IFeatureHandler<ForgotPasswordRequest, ForgotPasswordResponse>
    {
        public Task<ForgotPasswordResponse> ExecuteAsync(ForgotPasswordRequest command, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
