namespace Blablacar.Core.Entities;

/// <summary>
/// Сообщение
/// </summary>
public class Message : IEntity<int>
{
    /// <summary>
    /// Id сообщения
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Навигационное свойство 
    /// </summary>
    public string SenderId { get; set; } = default!;

    /// <summary>
    /// Отправитель
    /// </summary>
    public User Sender { get; set; } = default!;

    /// <summary>
    /// Навигационное свойство
    /// </summary>
    public string ReceiverId { get; set; } = default!;

    /// <summary>
    /// Получатель
    /// </summary>
    public User Receiver { get; set; } = default!;

    /// <summary>
    /// Текст
    /// </summary>
    public string Text { get; set; } = default!;
    
    /// <summary>
    /// Время отправки
    /// </summary>
    public DateTime Dispatch { get; set; }
}