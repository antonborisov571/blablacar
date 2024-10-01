using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Blablacar.Data.PostgreSQL.Configurations;
using Blablacar.Core.Abstractions.Services;
using Blablacar.Core.Entities;
using File = Blablacar.Core.Entities.File;

namespace Blablacar.Data.PostgreSQL;

/// <summary>
/// Контекст БД
/// </summary>
public class AppDbContext
    : IdentityDbContext<User>, IDbContext
{
    /// <summary>
    /// Конструктор
    /// </summary>
    public AppDbContext(DbContextOptions options)
        : base(options)
    {
    }

    /// <inheritdoc cref="EmailNotification"/>
    public DbSet<EmailNotification> EmailNotifications { get; set; }
    
    /// <inheritdoc cref="File"/>
    public DbSet<File> Files { get; set; }

    /// <inheritdoc cref="Trip"/>
    public DbSet<Trip> Trips { get; set; }

    /// <inheritdoc cref="TripNotification"/>
    public DbSet<TripNotification> TripNotifications { get; set; }

    /// <inheritdoc cref="Message"/>
    public DbSet<Message> Messages { get; set; }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new UserConfiguration());
        builder.ApplyConfiguration(new EmailNotificationConfiguration());
        builder.ApplyConfiguration(new FileConfiguration());
        builder.ApplyConfiguration(new TripConfiguration());
        builder.ApplyConfiguration(new TripNotificationConfiguration());
        builder.ApplyConfiguration(new MessageConfiguration());
        
        base.OnModelCreating(builder);
    }
}