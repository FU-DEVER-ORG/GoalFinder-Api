using GoalFinder.Application.Shared.Commons;
using GoalFinder.Application.Shared.Features;
using GoalFinder.Application.Shared.FIleObjectStorage;
using GoalFinder.Data.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GoalFinder.Application.Features.Auth.Register;

/// <summary>
///     Register as user request handler.
/// </summary>
internal sealed class RegisterAsUserHandler : IFeatureHandler<
    RegisterAsUserRequest,
    RegisterAsUserResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<Data.Entities.User> _userManager;
    private readonly IDefaultUserAvatarAsUrlHandler _defaultUserAvatarAsUrlHandler;

    public RegisterAsUserHandler(
        IUnitOfWork unitOfWork,
        UserManager<Data.Entities.User> userManager,
        IDefaultUserAvatarAsUrlHandler defaultUserAvatarAsUrlHandler)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _defaultUserAvatarAsUrlHandler = defaultUserAvatarAsUrlHandler;
    }

    /// <summary>
    ///     Entry of new request handler.
    /// </summary>
    /// <param name="command">
    ///     Request model.
    /// </param>
    /// <param name="ct">
    ///     A token that is used for notifying system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     A task containing the response.
    /// </returns>
    public async Task<RegisterAsUserResponse> ExecuteAsync(
        RegisterAsUserRequest command,
        CancellationToken ct)
    {
        // Does user exist by username.
        var isUserFound = await _unitOfWork.RegisterAsUserRepository
            .IsUserFoundByNormalizedEmailOrUsernameQueryAsync(
                userEmail: command.Email,
                cancellationToken: ct);

        // User with username already exists.
        if (isUserFound)
        {
            return new()
            {
                StatusCode = RegisterAsUserResponseStatusCode.USER_IS_EXISTED
            };
        }

        // Init new user.
        Data.Entities.User newUser = new()
        {
            Id = Guid.NewGuid()
        };

        // Is new user password valid.
        var isPasswordValid = await ValidateUserPasswordAsync(
            newUser: newUser,
            newPassword: command.Password);

        // Password is not valid.
        if (!isPasswordValid)
        {
            return new()
            {
                StatusCode = RegisterAsUserResponseStatusCode.USER_PASSWORD_IS_NOT_VALID
            };
        }

        // Completing new user.
        FinishFillingUser(
            newUser: newUser,
            command: command);

        // Create and add user to role.
        var dbResult = await _unitOfWork.RegisterAsUserRepository
            .CreateAndAddUserToRoleCommandAsync(
                newUser: newUser,
                userPassword: command.Password,
                userManager: _userManager,
                cancellationToken: ct);

        // Cannot create or add user to role.
        if (!dbResult)
        {
            return new()
            {
                StatusCode = RegisterAsUserResponseStatusCode.DATABASE_OPERATION_FAIL
            };
        }

        return new()
        {
            StatusCode = RegisterAsUserResponseStatusCode.OPERATION_SUCCESS
        };
    }

    /// <summary>
    ///     Validates the password of a newly created user.
    /// </summary>
    /// <param name="newUser">
    ///     The newly created user.
    /// </param>
    /// <param name="newPassword">
    ///     The password to be validated.
    /// </param>
    /// <returns>
    ///     True if the password is valid, False otherwise.
    /// </returns>
    private async Task<bool> ValidateUserPasswordAsync(
        Data.Entities.User newUser,
        string newPassword)
    {
        IdentityResult result = default;

        foreach (var validator in _userManager.PasswordValidators)
        {
            result = await validator.ValidateAsync(
                manager: _userManager,
                user: newUser,
                password: newPassword);
        }

        if (Equals(objA: result, objB: default))
        {
            return false;
        }

        return result.Succeeded;
    }

    /// <summary>
    ///     Finishes filling the user with default
    ///     values for the newly created user.
    /// </summary>
    /// <param name="newUser">
    ///     The newly created user.
    /// </param>
    private void FinishFillingUser(
        Data.Entities.User newUser,
        RegisterAsUserRequest command)
    {
        newUser.Email = command.Email;
        newUser.UserName = command.Email;
        newUser.UserDetail = new()
        {
            UserId = newUser.Id,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = newUser.Id,
            RemovedAt = DateTime.MinValue,
            RemovedBy = CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID,
            UpdatedAt = DateTime.MinValue,
            UpdatedBy = CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID,
            Address = string.Empty,
            CompetitionLevelId = CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID,
            FirstName = string.Empty,
            LastName = string.Empty,
            Description = string.Empty,
            ExperienceId = CommonConstant.App.DEFAULT_ENTITY_ID_AS_GUID,
            PrestigeScore = 100,
            AvatarUrl = _defaultUserAvatarAsUrlHandler.Get()
        };
    }
}
