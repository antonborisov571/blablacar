using Microsoft.Extensions.DependencyInjection;
using Blablacar.Core.Abstractions;
using Blablacar.Core.Abstractions.Repositories;
using Blablacar.Core.Abstractions.Services;
using Blablacar.Data.PostgreSQL.Repositories;

namespace Blablacar.Data.PostgreSQL;

/// <summary>
/// Входная точка
/// </summary>
public static class Entry
{
    /// <summary>
    /// Регистрация зависимостей
    /// </summary>
    public static void AddPostgreSqlLayout(this IServiceCollection serviceCollection)
    {
        if (serviceCollection is null)
            throw new ArgumentNullException(nameof(serviceCollection));

        serviceCollection.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Entry).Assembly));
        serviceCollection.AddDbContext<AppDbContext>();
        serviceCollection.AddScoped<IDbContext, AppDbContext>();
        serviceCollection.AddTransient<Migrator>();
        serviceCollection.AddScoped<AbstractFilesRepository, FilesRepository>();
        serviceCollection.AddScoped<AbstractTripsRepository, TripsRepository>();
        serviceCollection.AddScoped<AbstractEmailNotificationsRepository, EmailNotificationsRepository>();
        serviceCollection.AddScoped<AbstractTripNotificationsRepository, TripNotificationsRepository>();
        serviceCollection.AddScoped<AbstractMessagesRepository, MessagesRepository>();
        serviceCollection.AddLogging();
    } 
}