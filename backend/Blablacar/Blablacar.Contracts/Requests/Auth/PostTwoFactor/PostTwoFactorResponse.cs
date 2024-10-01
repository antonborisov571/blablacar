namespace Blablacar.Contracts.Requests.Auth.PostTwoFactor;

/// <summary>
/// Результат логина для PostLogin
/// </summary>
public class PostTwoFactorResponse
{
    /// <summary>
    /// JWT
    /// </summary>
    public string AccessToken { get; set; } = default!;

    /// <summary>
    /// Токен для обновления JWT
    /// </summary>
    public string RefreshToken { get; set; } = default!;
}