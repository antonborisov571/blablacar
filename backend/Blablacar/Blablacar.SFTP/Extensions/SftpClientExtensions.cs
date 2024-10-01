using Renci.SshNet.Abstractions.Sftp;

namespace Blablacar.SFTP.Extensions;

/// <summary>
/// Функциональности для <see cref="Renci.SshNet.SftpClient"/>
/// </summary>
public static class SftpClientExtensions
{
    /// <summary>
    /// Скачать файл
    /// </summary>
    /// <param name="client">SftpClient</param>
    /// <param name="path">Путь до файла в хранилище</param>
    /// <param name="output">Stream для записи</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    public static async Task DownloadFileAsync(this ISftpClient client, string path, Stream output, CancellationToken cancellationToken = default)
    {
        var task = Task.Factory.FromAsync(client.BeginDownloadFile(path, output), client.EndDownloadFile);
        await task;
    }

    /// <summary>
    /// Загрузить файл
    /// </summary>
    /// <param name="client">SftpClient</param>
    /// <param name="input">Stream откуда берется файл</param>
    /// <param name="path">Путь до файла</param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    public static async Task UploadFileAsync(this ISftpClient client, Stream input, string path, CancellationToken cancellationToken  = default)
    {
        var task = Task.Factory.FromAsync(client.BeginUploadFile(input, path), client.EndUploadFile);
        await task;
    }
}