using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Blablacar.Data.PostgreSQL;

/// <summary>
/// Мигратор для наката миграций и базовый значений
/// </summary>
public class Migrator
{
    private readonly AppDbContext _appDbContext;
    private readonly ILogger<Migrator> _logger;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="appDbContext">Контекст БД</param>
    /// <param name="logger">Логгер</param>
    public Migrator(
        AppDbContext appDbContext,
        ILogger<Migrator> logger)
    {
        _appDbContext = appDbContext;
        _logger = logger;
    }

    /// <summary>
    /// Мигратор
    /// </summary>
    public async Task MigrateAsync()
    {
        try
        {
            var migrateId = Guid.NewGuid().ToString();
            _logger.LogInformation($"Apply migrations started: {migrateId}");
            await _appDbContext.Database.MigrateAsync().ConfigureAwait(false);
            _logger.LogInformation($"Apply migrations succseffuly {migrateId}");
        }
        catch (Exception e)
        {
            _logger.LogCritical($"Failed apply migrations {e.Message}");
            throw;
        }
    }
}