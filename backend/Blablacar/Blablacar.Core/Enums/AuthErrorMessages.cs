namespace Blablacar.Core.Enums;

/// <summary>
/// Стандартные сообщения об ошибке авторизации/регистрации
/// </summary>
public static class AuthErrorMessages
{
    /// <summary>
    /// Пользователь не найден
    /// </summary>
    public const string UserNotFound = "Пользователь не найден";
    
    /// <summary>
    /// Почта не подтверждена
    /// </summary>
    public const string UnconfirmedEmail = "Почта не подтверждена";

    /// <summary>
    /// Неправильный пароль
    /// </summary>
    public const string WrongPassword = "Неправильный пароль";
    
    /// <summary>
    /// Пароли не совпадают
    /// </summary>
    public const string PasswordIsNotConfirmed = "Пароли не совпадают";
    
    /// <summary>
    /// Пользователь с такой почтой уже зарегистрирован
    /// </summary>
    public const string UserWithSameEmail = "Пользователь с такой почтой уже зарегистрирован";

    /// <summary>
    /// Пустое Required поле в Request
    /// </summary>
    /// <param name="fieldName"></param>
    /// <returns>Empty field error message</returns>
    public static string EmptyField(string fieldName) => $"{fieldName} не может быть пустым";

    /// <summary>
    /// Неправильный формат почты
    /// </summary>
    public const string InvalidEmailFormat = "Неверный формат почты";

    /// <summary>
    /// Новый пароль и старый совпадают
    /// </summary>
    public const string EqualsOldAndNewPasswords = "Новый и старый пароль совпадают";

    /// <summary>
    /// Пароль короче минимальной длины
    /// </summary>
    /// <param name="requiredLength">Минимальная длина пароля</param>
    /// <returns></returns>
    public static string ShortPassword(int requiredLength) 
        => $"Пароль короче минимальной длины: {requiredLength} ";

    /// <summary>
    /// Некорректный JWT
    /// </summary>
    public const string InvalidAccessToken = "Некорректный JWT";
    
    /// <summary>
    /// Некорректный Refresh Token
    /// </summary>
    public const string InvalidRefreshToken = "Некорректный Refresh Token";

    /// <summary>
    /// Неверный код подтверждения
    /// </summary>
    public const string WrongConfirmationToken = "Неверный код подтверждения";

    /// <summary>
    /// Неподтверждённая почта
    /// </summary>
    public const string NotConfirmedEmail = "Неподтверждённая почта";

    /// <summary>
    /// Если в Claim'ах не пришла почта
    /// </summary>
    public const string EmailClaimNotFound = "Почта не найдена";

    /// <summary>
    /// Если не найдена логин информация при логине через сторонние сервисы
    /// </summary>
    public const string ExternalLoginInfoNotFound = "Не найдена логин информация при логине через сторонние сервисы";
}