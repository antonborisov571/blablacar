namespace Blablacar.Contracts.Requests.OAuth.GetExternalLoginCallback;

/// <summary>
/// Ответ на авторизацию через сторонние сервисы
/// </summary>
public class GetExternalLoginCallbackResponse
{
    /// <summary>
    /// JWT
    /// </summary>
    public string AccessToken { get; set; } = default!;

    /// <summary>
    /// Refresh Token
    /// </summary>
    public string RefreshToken { get; set; } = default!;
}