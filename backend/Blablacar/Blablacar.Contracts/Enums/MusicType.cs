using System.ComponentModel;
using System.Runtime.Serialization;

namespace Blablacar.Contracts.Enums;

/// <summary>
/// Предпочтения о музыке
/// </summary>
public enum MusicType
{
    /// <summary>
    /// Включайте, и погромче!
    /// </summary>
    [Description("Включайте, и погромче!")]
    LowMusic,
    
    /// <summary>
    /// Все зависит от настроения — могу и спеть!
    /// </summary>
    [Description("Все зависит от настроения — могу и спеть!")]
    MiddleMusic,
    
    /// <summary>
    /// Предпочитаю тишину
    /// </summary>
    [Description("Предпочитаю тишину")]
    HighMusic,
}