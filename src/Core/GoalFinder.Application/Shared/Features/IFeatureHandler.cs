using FastEndpoints;

namespace GoalFinder.Application.Shared.Features;

public interface IFeatureHandler<TRequest, TResponse> : ICommandHandler<TRequest, TResponse>
    where TRequest : class, IFeatureRequest<TResponse>
    where TResponse : class, IFeatureResponse { }
