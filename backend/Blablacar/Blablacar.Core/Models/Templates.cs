namespace Blablacar.Core.Models;

/// <summary>
/// Шаблон сообщений для почты
/// </summary>
public static class Templates
{
    /// <summary>
    /// Отпрака сообщения с токеном подтверждения почты
    /// </summary>
    public const string SendEmailConfirmationMessage = "SendEmailConfirmationMessage.html";

    /// <summary>
    /// Отправка сообщения для создания нового пароля
    /// </summary>
    public const string SendForgotPasswordMessage = "SendForgotPasswordMessage.html";
    
    /// <summary>
    /// Отправка сообщения для сброса пароля
    /// </summary>
    public const string SendPasswordResetConfirmationMessage = "SendPasswordResetConfirmationMessage.html";

    /// <summary>
    /// Отправка уведомления о том, что данные аккаунта были изменены
    /// </summary>
    public const string SendUserInfoUpdatedNotification = "SendUserInfoUpdatedNotification.html";
    
    /// <summary>
    /// Отправка уведомления о том, что есть новая поездка
    /// </summary>
    public const string SendTripNotification = "SendTripNotification.html";
    
    /// <summary>
    /// Отправка уведомления о том, что есть новая поездка
    /// </summary>
    public const string SendMessageNotification = "SendMessageNotification.html";
    
    /// <summary>
    /// Отправка уведомления о том, что забронировали поездку
    /// </summary>
    public const string SendBookingNotification = "SendBookingNotification.html";
    
    /// <summary>
    /// Отправка уведомления об отказе в поездке
    /// </summary>
    public const string SendDeletePassengerNotification = "SendDeletePassengerNotification.html";
}
