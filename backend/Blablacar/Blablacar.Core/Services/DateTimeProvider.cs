using Blablacar.Core.Abstractions.Services;

namespace Blablacar.Core.Services;

/// <summary>
/// Провайдер дат
/// </summary>
public class DateTimeProvider : IDateTimeProvider
{
    /// <inheritdoc />
    public DateTime CurrentDate => DateTime.UtcNow;
}