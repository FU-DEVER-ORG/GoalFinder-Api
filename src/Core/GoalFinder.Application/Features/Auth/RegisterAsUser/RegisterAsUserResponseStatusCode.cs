namespace GoalFinder.Application.Features.Auth.Register;

/// <summary>
///     Register as user response status code.
/// </summary>
public enum RegisterAsUserResponseStatusCode
{
    USER_IS_EXISTED,
    USER_PASSWORD_IS_NOT_VALID,
    INPUT_VALIDATION_FAIL,
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL
}
