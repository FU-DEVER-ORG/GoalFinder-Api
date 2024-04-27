using FluentValidation;
using GoalFinder.Application.Shared.Features;

namespace GoalFinder.Application.Features.User.UpdateUserInfo;

/// <summary>
///     Update user info request validator.
/// </summary>
public sealed class UpdateUserInfoRequestValidator :
    FeatureRequestValidator<
        UpdateUserInfoRequest,
        UpdateUserInfoResponse>
{
    public UpdateUserInfoRequestValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(expression: request => request.UserName)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .NotEmpty()
            .MaximumLength(maximumLength: Data.Entities.User.MetaData.UserName.MaxLength)
            .MinimumLength(minimumLength: Data.Entities.User.MetaData.UserName.MinLength);

        RuleFor(expression: request => request.FirstName)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .NotEmpty()
            .MaximumLength(maximumLength: Data.Entities.UserDetail.MetaData.FirstName.MaxLength)
            .MinimumLength(minimumLength: Data.Entities.UserDetail.MetaData.FirstName.MinLength);

        RuleFor(expression: request => request.LastName)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .NotEmpty()
            .MaximumLength(maximumLength: Data.Entities.UserDetail.MetaData.FirstName.MaxLength)
            .MinimumLength(minimumLength: Data.Entities.UserDetail.MetaData.FirstName.MinLength);

        RuleFor(expression: request => request.Description)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .MaximumLength(maximumLength: Data.Entities.UserDetail.MetaData.FirstName.MaxLength);

        RuleFor(expression: request => request.Address)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .NotEmpty()
            .MaximumLength(maximumLength: Data.Entities.UserDetail.MetaData.Address.MaxLength)
            .MinimumLength(minimumLength: Data.Entities.UserDetail.MetaData.Address.MinLength);

        RuleFor(expression: request => request.AvatarUrl)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .NotEmpty()
            .MinimumLength(minimumLength: Data.Entities.UserDetail.MetaData.AvatarUrl.MinLength);

        RuleFor(expression: request => request.BackgroundUrl)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .NotEmpty()
            .MinimumLength(minimumLength: Data.Entities.UserDetail.MetaData.BackgroundUrl.MinLength);

        RuleFor(expression: request => request.ExperienceId)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .NotEmpty();

        RuleForEach(expression: request => request.PositionIds)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .NotEmpty();

        RuleFor(expression: request => request.CompetitionLevelId)
            .Cascade(cascadeMode: CascadeMode.Stop)
            .NotEmpty();
    }
}
