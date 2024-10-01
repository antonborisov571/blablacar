namespace Blablacar.Core.Abstractions.Services;

/// <summary>
/// Контекс текущего пользоавтеля
/// </summary>
public interface IUserContext
{
    /// <summary>
    /// ИД текущего пользователя
    /// </summary>
    Guid? CurrentUserId { get; }
}