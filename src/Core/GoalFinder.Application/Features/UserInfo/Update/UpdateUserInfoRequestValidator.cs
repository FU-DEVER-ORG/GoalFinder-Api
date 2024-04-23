using FluentValidation;
using GoalFinder.Application.Shared.Features;

namespace GoalFinder.Application.Features.UserInfo.Update
{
    /// <summary>
    ///     Update user request validator.
    /// </summary>
    public sealed class UpdateUserInfoRequestValidator : FeatureRequestValidator<UpdateUserInfoRequest, UpdateUserInfoResponse>
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
                .MinimumLength(minimumLength: Data.Entities.UserDetail.MetaData.FirstName.MinLength);

            RuleFor(expression: request => request.Experience)
                .Cascade(cascadeMode: CascadeMode.Stop)
                .NotEmpty()
                .MaximumLength(maximumLength: Data.Entities.Experience.MetaData.FullName.MaxLength)
                .MinimumLength(minimumLength: Data.Entities.Experience.MetaData.FullName.MinLength);

            RuleFor(expression: request => request.Position)
               .Cascade(cascadeMode: CascadeMode.Stop)
               .NotEmpty();

            RuleFor(expression: request => request.CompetitionLevel)
                .Cascade(cascadeMode: CascadeMode.Stop)
                .NotEmpty()
                .MaximumLength(maximumLength: Data.Entities.CompetitionLevel.MetaData.FullName.MaxLength)
                .MinimumLength(minimumLength: Data.Entities.CompetitionLevel.MetaData.FullName.MinLength);



        }
    }
}
