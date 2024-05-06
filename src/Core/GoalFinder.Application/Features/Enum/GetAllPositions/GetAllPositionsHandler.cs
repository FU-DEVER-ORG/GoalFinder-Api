using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoalFinder.Application.Shared.Features;
using GoalFinder.Data.UnitOfWork;

namespace GoalFinder.Application.Features.Enum.GetAllPositions;

/// <summary>
///     Handler for get all positions features.
/// </summary>
internal sealed class GetAllPositionsHandler
    : IFeatureHandler<GetAllPositionsRequest, GetAllPositionsResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllPositionsHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetAllPositionsResponse> ExecuteAsync(
        GetAllPositionsRequest command,
        CancellationToken ct
    )
    {
        var positions = await _unitOfWork.GetAllPositionsRepository.GetAllPositionsQueryAsync(
            cancellationToken: ct
        );

        return new()
        {
            StatusCode = GetAllPositionsResponseStatusCode.OPERATION_SUCCESS,
            Body = new()
            {
                Positions = positions.Select(
                    position => new GetAllPositionsResponse.ResponseBody.Position
                    {
                        PositionId = position.Id,
                        Name = position.FullName,
                    }
                )
            }
        };
    }
}
