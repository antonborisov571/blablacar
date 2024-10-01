namespace Blablacar.SFTP.Exceptions;

/// <summary>
/// Если не удалось подключиться
/// </summary>
/// <param name="message">Сообщение</param>
public class ConnectionFailedException(string message) : Exception(message);