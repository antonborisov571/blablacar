namespace Blablacar.Contracts.Requests.Auth.PostRegister;

/// <summary>
/// Запрос на регистрацию
/// </summary>
public class PostRegisterRequest
{
    /// <summary>
    /// Конструктор с передачей объекта
    /// </summary>
    /// <param name="request"><see cref="PostRegisterRequest"/></param>
    /// <exception cref="ArgumentNullException">Если request - null</exception>
    public PostRegisterRequest(PostRegisterRequest request)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        FirstName = request.FirstName;
        LastName = request.LastName;
        Birthday = request.Birthday;
        Password = request.Password;
        PasswordConfirm = request.PasswordConfirm;
        Email = request.Email;
    }
    
    /// <summary>
    /// Конструктор
    /// </summary>
    public PostRegisterRequest()
    {
    }
    
    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string FirstName { get; set; } = default!;
    
    /// <summary>
    /// Фамилия пользователя
    /// </summary>
    public string LastName { get; set; } = default!;

    /// <summary>
    /// Пароль
    /// </summary>
    public string Password { get; set; } = default!;

    /// <summary>
    /// Подтверждение пароля
    /// </summary>
    public string PasswordConfirm { get; set; } = default!;

    /// <summary>
    /// Почта
    /// </summary>
    public string Email { get; set; } = default!;

    /// <summary>
    /// Дата рождения
    /// </summary>
    public DateTime Birthday { get; set; }
}