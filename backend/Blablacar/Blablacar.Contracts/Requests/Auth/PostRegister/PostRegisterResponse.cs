namespace Blablacar.Contracts.Requests.Auth.PostRegister;

/// <summary>
/// Ответ на запрос на регистрацию
/// </summary>
public class PostRegisterResponse
{
    /// <summary>
    /// Почта пользователя
    /// </summary>
    public string Email { get; set; } = default!;
}