namespace Blablacar.Contracts.Requests.Auth.PostResetPassword;

/// <summary>
/// Запрос для обновления пароля
/// </summary>
public class PostResetPasswordRequest
{
    /// <summary>
    /// Конструктор с передачей объекта
    /// </summary>
    /// <param name="request"><see cref="PostResetPasswordRequest"/></param>
    /// <exception cref="ArgumentNullException">Если request - null</exception>
    public PostResetPasswordRequest(PostResetPasswordRequest request)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        Email = request.Email;
        NewPassword = request.NewPassword;
        NewPasswordConfirm = request.NewPasswordConfirm;
    }
    
    /// <summary>
    /// Конструктор
    /// </summary>
    public PostResetPasswordRequest()
    {
    }
    
    /// <summary>
    /// Email пользователя
    /// </summary>
    public string Email { get; set; } = default!;

    /// <summary>
    /// Новый пароль
    /// </summary>
    public string NewPassword { get; set; } = default!;

    /// <summary>
    /// Подтверждение нового пароля
    /// </summary>
    public string NewPasswordConfirm { get; set; } = default!;
}