using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoalFinder.Application.Shared.Features;
using GoalFinder.Data.Entities;
using GoalFinder.Data.UnitOfWork;

namespace GoalFinder.Application.Features.Enum.GetAllCompetitionLevels;

/// <summary>
///     Handler for get all competitionLevels features.
/// </summary>
internal sealed class GetAllCompetitionLevelsHandler
    : IFeatureHandler<GetAllCompetitionLevelsRequest, GetAllCompetitionLevelsResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllCompetitionLevelsHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetAllCompetitionLevelsResponse> ExecuteAsync(
        GetAllCompetitionLevelsRequest command,
        CancellationToken ct
    )
    {
        var CompetitionLevels = await _unitOfWork.GetAllCompetitionLevelsRepository.GetAllCompetitionLevelsQueryAsync(
            cancellationToken: ct
        );

        return new()
        {
            StatusCode = GetAllCompetitionLevelsResponseStatusCode.OPERATION_SUCCESS,
            Body = new()
            {
                CompetitionLevels = CompetitionLevels.Select(
                    competitionLevel => new GetAllCompetitionLevelsResponse.ResponseBody.CompetitionLevel
                    {
                        CompetitionLevelId = competitionLevel.Id,
                        Name = competitionLevel.FullName,
                    }
                )
            }
        };
    }
}
