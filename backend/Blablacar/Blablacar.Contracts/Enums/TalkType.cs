using System.ComponentModel;

namespace Blablacar.Contracts.Enums;

/// <summary>
/// Разговорчивость
/// </summary>
public enum TalkType
{
    /// <summary>
    /// Люблю поболтать!
    /// </summary>
    [Description("Люблю поболтать!")]
    LowTalk,
    
    /// <summary>
    /// Не прочь поболтать, когда мне комфортно
    /// </summary>
    [Description("Не прочь поболтать, когда мне комфортно")]
    MiddleTalk,
    
    /// <summary>
    /// Я скорее тихоня
    /// </summary>
    [Description("Я скорее тихоня")]
    HighTalk,
}