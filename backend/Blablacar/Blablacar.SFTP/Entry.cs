using Blablacar.Core.Abstractions;
using Blablacar.Core.Abstractions.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Renci.SshNet.Abstractions.Sftp;
using ISftpClient = Renci.SshNet.ISftpClient;

namespace Blablacar.SFTP;

/// <summary>
/// Подключение зависимостей для Sftp
/// </summary>
public static class Entry
{
    /// <summary>
    /// Добавить зависимости
    /// </summary>
    /// <param name="serviceCollection"><see cref="IServiceCollection"/></param>
    /// <param name="options"><see cref="SftpOptions"/></param>
    /// <returns><see cref="IServiceCollection"/></returns>
    public static IServiceCollection AddSftpStorage(
        this IServiceCollection serviceCollection, SftpOptions options)
    {
        if (options is null)
            throw new ArgumentNullException(nameof(options));

        if (string.IsNullOrEmpty(options.Host))
            throw new ArgumentException(nameof(options.Host));

        if (string.IsNullOrEmpty(options.User))
            throw new AggregateException(nameof(options.User));

        if (string.IsNullOrEmpty(options.Password))
            throw new ArgumentException(nameof(options.Password));
        
        serviceCollection.AddSingleton(options);
        serviceCollection.AddSingleton<ISftpClientFactory, SftpClientFactory>();
        serviceCollection.AddSingleton<ISftpService, SftpService>();
        

        return serviceCollection;
    }
}