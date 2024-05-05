using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoalFinder.Application.Shared.Features;
using GoalFinder.Data.UnitOfWork;

namespace GoalFinder.Application.Features.Enum.GetAllExperiences;

/// <summary>
///     Handler for get all Experiences features.
/// </summary>
internal sealed class GetAllExperiencesHandler
    : IFeatureHandler<GetAllExperiencesRequest, GetAllExperiencesResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllExperiencesHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<GetAllExperiencesResponse> ExecuteAsync(
        GetAllExperiencesRequest command,
        CancellationToken ct
    )
    {
        var Experiences = await _unitOfWork.GetAllExperiencesRepository.GetAllExperiencesQueryAsync(
            cancellationToken: ct
        );

        return new()
        {
            StatusCode = GetAllExperiencesResponseStatusCode.OPERATION_SUCCESS,
            Body = new()
            {
                Experiences = Experiences.Select(
                    experience => new GetAllExperiencesResponse.ResponseBody.Experience
                    {
                        ExperienceId = experience.Id,
                        Name = experience.FullName,
                    }
                )
            }
        };
    }
}
