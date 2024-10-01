using Blablacar.Contracts.Requests.Auth.PostConfirmEmail;

namespace Blablacar.Contracts.Requests.Auth.PostTwoFactor;

/// <summary>
/// Запрос на двухфакторку
/// </summary>
public class PostTwoFactorRequest
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="request"><see cref="PostTwoFactorRequest"/></param>
    /// <exception cref="ArgumentNullException">Если request - null</exception>
    public PostTwoFactorRequest(PostTwoFactorRequest request)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        Email = request.Email;
        Code = request.Code;
    }
    
    /// <summary>
    /// Конструктор
    /// </summary>
    public PostTwoFactorRequest()
    {
    }
    
    /// <summary>
    /// Почта пользователя
    /// </summary>
    public string Email { get; set; } = default!;
    
    /// <summary>
    /// Код для двухфакторки
    /// </summary>
    public string Code { get; set; } = default!;
}