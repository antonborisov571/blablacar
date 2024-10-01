using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Blablacar.Core.Models;
using Blablacar.Worker.Workers;

namespace Blablacar.Worker;

/// <summary>
/// Точка входа для воркера
/// </summary>
public static class Entry
{
    /// <summary>
    /// Добавить службу с тасками по расписанию
    /// </summary>
    /// <param name="serviceCollection"></param>
    /// <returns></returns>
    public static IServiceCollection AddHangfireWorker(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddHangfire(x => x.UseMemoryStorage());
        return serviceCollection.AddHangfireServer();
    }

    /// <summary>
    /// Использование Middleware для Hangfire
    /// </summary>
    /// <returns><see cref="IApplicationBuilder"/><see cref="IApplicationBuilder"/></returns>
    /// <exception cref="ArgumentNullException">Если нет настроек</exception>    
    public static IApplicationBuilder UseHangfireWorker(
        this IApplicationBuilder app,
        HangfireOptions options)
    {
        if (options is null)
            throw new ArgumentNullException(nameof(options));
        
        if (options.DisplayDashBoard)
            app.UseHangfireDashboard("/worker", new DashboardOptions
            {
                Authorization = new[] { new DashboardAuthorizationFilter() },
            });

        app.UseHangfireServer();
        
        AddJob<EmailNotificator>(options.CronForSendEmailNotificator);
        
        AddJob<DeleteTripNotifications>(options.CronForDeleteTripNotification);
        
        return app;
    }

    /// <summary>
    /// Добавить задачу
    /// </summary>
    /// <param name="cron">Крон</param>
    /// <typeparam name="T">Задача</typeparam>
    private static void AddJob<T>(string cron)
        where T : IWorker
        => RecurringJob.AddOrUpdate<T>(
            typeof(T).FullName,
            (x) => x.RunAsync(),
            cron,
            new RecurringJobOptions
            {
                TimeZone = TimeZoneInfo.Utc
            });
}