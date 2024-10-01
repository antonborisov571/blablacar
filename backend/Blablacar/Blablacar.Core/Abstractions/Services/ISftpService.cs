using Blablacar.Core.Models;

namespace Blablacar.Core.Abstractions.Services;

/// <summary>
/// Сервис Sftp
/// </summary>
public interface ISftpService
{
    /// <summary>
    /// Загрузить файл в хранилище
    /// </summary>
    /// <param name="fileContent">Бинарные данные</param>
    /// <param name="needAutoCloseStream">Нужно ли закрывать поток после загрузки</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns></returns>
    public Task<string> UploadAsync(
        FileContent fileContent,
        bool needAutoCloseStream = true,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Скачать файл по ключу
    /// </summary>
    /// <param name="address">Ключ</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Файл</returns>
    public Task<FileContent?> DownloadFileAsync(
        string address,
        CancellationToken cancellationToken = default);
}