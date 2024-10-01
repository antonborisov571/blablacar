namespace Blablacar.Contracts.Requests.Auth.PostConfirmEmail;

/// <summary>
/// Запрос для подтверждения почты
/// </summary>
public class PostConfirmEmailRequest
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="request"><see cref="PostConfirmEmailRequest"/></param>
    /// <exception cref="ArgumentNullException">Если request - null</exception>
    public PostConfirmEmailRequest(PostConfirmEmailRequest request)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        Email = request.Email;
        Code = request.Code;
    }
    
    /// <summary>
    /// Конструктор
    /// </summary>
    public PostConfirmEmailRequest()
    {
    }
    
    /// <summary>
    /// Почта пользователя
    /// </summary>
    public string Email { get; set; } = default!;
    
    /// <summary>
    /// Код для подтверждения почты
    /// </summary>
    public string Code { get; set; } = default!;
}