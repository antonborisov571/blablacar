namespace Blablacar.Contracts.Requests.Auth.PostRefreshToken;

/// <summary>
/// Запрос для обновления токена
/// </summary>
public class PostRefreshTokenRequest
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="request"><see cref="PostRefreshTokenRequest"/></param>
    /// <exception cref="ArgumentNullException">Если request - null</exception>
    public PostRefreshTokenRequest(PostRefreshTokenRequest request)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        AccessToken = request.AccessToken;
        RefreshToken = request.RefreshToken;
    }

    /// <summary>
    /// Конструктор
    /// </summary>
    public PostRefreshTokenRequest()
    {
    }
    
    /// <summary>
    /// JWT
    /// </summary>
    public string AccessToken { get; set; } = default!;

    /// <summary>
    /// Токен для обновления JWT
    /// </summary>
    public string RefreshToken { get; set; } = default!;
}