using Blablacar.Core.Abstractions.Services;
using Blablacar.Core.Entities;

namespace Blablacar.Core.Abstractions.Repositories;

/// <summary>
/// Репозиторий сообщений
/// </summary>
/// <param name="dbContext">Контекст базы данных</param>
public abstract class AbstractMessagesRepository(IDbContext dbContext)
    : GenericRepository<Message, int>(dbContext)
{
    /// <summary>
    /// Получить сообщения
    /// </summary>
    /// <param name="senderId">Id отправителя</param>
    /// <param name="receiverId">Id получателя</param>
    /// <returns>Сообщения</returns>
    public abstract Task<List<Message>> GetMessagesByReceiver(string senderId, string receiverId);

    /// <summary>
    /// Получить чаты
    /// </summary>
    /// <param name="userId">Id пользователя</param>
    /// <returns>Чаты</returns>
    public abstract List<Message> GetChats(string userId);
}