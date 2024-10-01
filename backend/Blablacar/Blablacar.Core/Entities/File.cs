namespace Blablacar.Core.Entities;

/// <summary>
/// Файл
/// </summary>
public class File : IEntity<Guid>
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="fileName">Имя файла</param>
    /// <param name="contentType">Тип</param>
    /// <param name="address">Адрес в хранилище</param>
    /// <param name="size">Размер</param>
    public File(
        string fileName,
        string contentType,
        string address,
        long size)
    {
        FileName = fileName;
        ContentType = contentType;
        Address = address;
        Size = size;
    }
    
    /// <summary>
    /// Конструктор
    /// </summary>
    public File()
    {
    }
    
    /// <summary>
    /// Ид файла
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Адрес на файл в sftp storage
    /// </summary>
    public string Address { get; protected set; } = default!;

    /// <summary>
    /// Размер файла
    /// </summary>
    public long Size { get; protected set; }

    /// <summary>
    /// Название файла
    /// </summary>
    public string? FileName { get; protected set; }

    /// <summary>
    /// Тип файла
    /// </summary>
    public string? ContentType { get; protected set; }
}