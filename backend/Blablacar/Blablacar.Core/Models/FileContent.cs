namespace Blablacar.Core.Models;

/// <summary>
/// Файл для Sftp
/// </summary>
public class FileContent
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="content">Бинарные данные</param>
    /// <param name="fileName">Название файла</param>
    /// <param name="contentType">Тип контента</param>
    public FileContent(
        Stream content,
        string fileName,
        string? contentType)
    {
        Content = content;
        FileName = fileName;
        ContentType = contentType ?? "application/octet-stream";
    }
    
    /// <summary>
    /// Конструктор
    /// </summary>
    public FileContent()
    {
    }

    /// <summary>
    /// Бинарные данные файла
    /// </summary>
    public Stream Content { get; set; } = default!;

    /// <summary>
    /// Название файла
    /// </summary>
    public string FileName { get; set; } = default!;

    /// <summary>
    /// Тип контента
    /// </summary>
    public string ContentType { get; set; } = default!;
}