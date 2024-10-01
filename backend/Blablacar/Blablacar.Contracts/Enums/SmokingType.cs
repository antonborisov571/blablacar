using System.ComponentModel;

namespace Blablacar.Contracts.Enums;

/// <summary>
/// Предпочтения о курении
/// </summary>
public enum SmokingType
{
    /// <summary>
    /// Я не против, если кто-то закурит
    /// </summary>
    [Description("Я не против, если кто-то закурит")]
    LowSmoking,
    
    /// <summary>
    /// Можно курить, но не в машине
    /// </summary>
    [Description("Можно курить, но не в машине")]
    MiddleSmoking,
    
    /// <summary>
    /// В моей машине не курят
    /// </summary>
    [Description("В моей машине не курят")]
    HighSmoking,
}