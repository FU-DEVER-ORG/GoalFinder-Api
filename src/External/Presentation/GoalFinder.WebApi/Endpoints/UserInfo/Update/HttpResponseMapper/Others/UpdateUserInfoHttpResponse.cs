using System.Collections.Generic;
using System.Text.Json.Serialization;
using System;
using GoalFinder.Application.Features.UserInfo.Update;

namespace GoalFinder.WebApi.Endpoints.UserInfo.Update.HttpResponseMapper.Others
{
    public class UpdateUserInfoHttpResponse
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int HttpCode { get; set; }

        public string AppCode { get; init; } = UpdateUserInfoResponseStatusCode.UPDATE_SUCCESS.ToAppCode();

        public DateTime ResponseTime { get; init; } = TimeZoneInfo.ConvertTimeFromUtc(
            dateTime: DateTime.UtcNow,
            destinationTimeZone: TimeZoneInfo.FindSystemTimeZoneById(id: "SE Asia Standard Time"));

        public object Body { get; init; } = new();

        public IEnumerable<string> ErrorMessages { get; init; } = [];
    }
}
