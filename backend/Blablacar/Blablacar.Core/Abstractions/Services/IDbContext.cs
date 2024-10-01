using Blablacar.Core.Entities;
using Microsoft.EntityFrameworkCore;
using File = Blablacar.Core.Entities.File;

namespace Blablacar.Core.Abstractions.Services;

/// <summary>
/// Интерфейс контекста бд
/// </summary>
public interface IDbContext
{
    /// <summary>
    /// Пользователи
    /// </summary>
    public DbSet<User> Users { get; set; }
    
    /// <summary>
    /// Уведомления на почту
    /// </summary>
    public DbSet<EmailNotification> EmailNotifications { get; set; }
    
    /// <summary>
    /// Файлы
    /// </summary>
    public DbSet<File> Files { get; set; }
    
    /// <summary>
    /// Поездки
    /// </summary>
    public DbSet<Trip> Trips { get; set; }
    
    /// <summary>
    /// Уведомления о поездках
    /// </summary>
    public DbSet<TripNotification> TripNotifications { get; set; }
    
    /// <summary>
    /// Сообщения
    /// </summary>
    public DbSet<Message> Messages { get; set; }
    
    /// <inheritdoc cref="DbContext.Set{TEntity}()"/>
    public DbSet<T> Set<T>() where T: class;
    
    /// <inheritdoc cref="DbContext.SaveChangesAsync(System.Threading.CancellationToken)"/>
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}