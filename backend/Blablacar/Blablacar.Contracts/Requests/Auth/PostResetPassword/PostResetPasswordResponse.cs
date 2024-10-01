namespace Blablacar.Contracts.Requests.Auth.PostResetPassword;

/// <summary>
/// Ответ на обновления пароля
/// </summary>
public class PostResetPasswordResponse
{
    /// <summary>
    /// Почта пользователя
    /// </summary>
    public string Email { get; set; } = default!;

    /// <summary>
    /// Новый пароль пользователя
    /// </summary>
    public string NewPassword { get; set; } = default!;
}