using Blablacar.Core.Entities;

namespace Blablacar.Core.Abstractions.Services;

/// <summary>
/// Сервис для работы с аватарками
/// </summary>
public interface IAvatarService
{
    /// <summary>
    /// Получить строку аватара
    /// </summary>
    /// <param name="user">Пользователь</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>Картинка в base64</returns>
    public Task<string?> GetAvatar(User user, CancellationToken cancellationToken = default);
}