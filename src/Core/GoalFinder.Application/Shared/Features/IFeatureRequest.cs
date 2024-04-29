using FastEndpoints;

namespace GoalFinder.Application.Shared.Features;

public interface IFeatureRequest<out TResponse> : ICommand<TResponse>
    where TResponse : class, IFeatureResponse { }
