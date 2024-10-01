namespace Blablacar.Contracts.Requests.Auth.PostForgotPassword;

/// <summary>
/// Запрос, если пользователь забыл пароль
/// </summary>
public class PostForgotPasswordRequest
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="request"><see cref="PostForgotPasswordRequest"/></param>
    /// <exception cref="ArgumentNullException">Если request - null</exception>
    public PostForgotPasswordRequest(PostForgotPasswordRequest request)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        Email = request.Email;
    }
    
    /// <summary>
    /// Конструктор
    /// </summary>
    public PostForgotPasswordRequest()
    {
    }
    
    /// <summary>
    /// Email пользователя
    /// </summary>
    public string Email { get; set; } = default!;
}