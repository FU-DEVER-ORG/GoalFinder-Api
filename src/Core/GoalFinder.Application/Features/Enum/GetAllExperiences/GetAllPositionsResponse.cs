using System;
using System.Collections.Generic;
using GoalFinder.Application.Shared.Features;

namespace GoalFinder.Application.Features.Enum.GetAllExperiences;

/// <summary>
///     Response for get all experiences features.
/// </summary>
public sealed class GetAllExperiencesResponse : IFeatureResponse
{
    public GetAllExperiencesResponseStatusCode StatusCode { get; init; }

    public ResponseBody Body { get; init; }

    public sealed class ResponseBody
    {
        public IEnumerable<Experience> Experiences { get; set; }

        public sealed class Experience
        {
            public Guid ExperienceId { get; init; }

            public string Name { get; init; }
        }
    }
}
