namespace Blablacar.Core.Abstractions.Services;

/// <summary>
/// Провайдер даты
/// </summary>
public interface IDateTimeProvider
{
    
    /// <summary>
    /// Текущая дата
    /// </summary>
    DateTime CurrentDate { get; }
}