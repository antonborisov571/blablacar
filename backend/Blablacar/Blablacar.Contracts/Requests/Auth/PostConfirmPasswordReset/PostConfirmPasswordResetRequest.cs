namespace Blablacar.Contracts.Requests.Auth.PostConfirmPasswordReset;

/// <summary>
/// Запрос для сброса пароля
/// </summary>
public class PostConfirmPasswordResetRequest
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="request"><see cref="PostConfirmPasswordResetRequest"/></param>
    /// <exception cref="ArgumentNullException">Если request - null</exception>
    public PostConfirmPasswordResetRequest(PostConfirmPasswordResetRequest request)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        NewPassword = request.NewPassword;
        Email = request.Email;
        VerificationCodeFromUser = request.VerificationCodeFromUser;
    }

    /// <summary>
    /// Конструктор
    /// </summary>
    public PostConfirmPasswordResetRequest()
    {
    }
    
    /// <summary>
    /// Email пользователя
    /// </summary>
    public string Email { get; set; } = default!;

    /// <summary>
    /// Код подтверждение для сброса пароля
    /// </summary>
    public string VerificationCodeFromUser { get; set; } = default!;

    /// <summary>
    /// Новый пароль
    /// </summary>
    public string NewPassword { get; set; } = default!;
}