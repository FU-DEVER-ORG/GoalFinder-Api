using FastEndpoints;

namespace GoalFinder.Application.Shared.Features;

public abstract class FeatureRequestValidator<TRequest, TResponse> :
    Validator<TRequest>
        where TRequest :
            class,
            IFeatureRequest<TResponse>
        where TResponse :
            class,
            IFeatureResponse
{
}
