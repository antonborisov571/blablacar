namespace Blablacar.SFTP;

/// <summary>
/// Параметры для соединения
/// </summary>
public class SftpOptions
{
    /// <summary>
    /// Ip для соединения
    /// </summary>
    public string Host { get; set; } = default!;
    
    /// <summary>
    /// Port
    /// </summary>
    public int Port { get; set; } = default!;
    
    /// <summary>
    /// Логин
    /// </summary>
    public string User { get; set; } = default!;
    
    /// <summary>
    /// Пароль
    /// </summary>
    public string Password { get; set; } = default!;
}