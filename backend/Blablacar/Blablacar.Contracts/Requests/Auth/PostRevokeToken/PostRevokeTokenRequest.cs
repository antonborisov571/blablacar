namespace Blablacar.Contracts.Requests.Auth.PostRevokeToken;

/// <summary>
/// Удалить RefreshToken
/// </summary>
public class PostRevokeTokenRequest
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="request"><see cref="PostRevokeTokenRequest"/></param>
    /// <exception cref="ArgumentNullException">Если request - null</exception>
    public PostRevokeTokenRequest(PostRevokeTokenRequest request)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        Email = request.Email;
    }
    
    /// <summary>
    /// Конструктор
    /// </summary>
    public PostRevokeTokenRequest()
    {
    }
    
    /// <summary>
    /// Email пользователя
    /// </summary>
    public string Email { get; set; } = default!;
}