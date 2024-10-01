using System.ComponentModel;

namespace Blablacar.Contracts.Enums;

/// <summary>
/// Предпочтения о животных
/// </summary>
public enum AnimalType
{
    /// <summary>
    /// Обожаю животных. Гав!
    /// </summary>
    [Description("Обожаю животных. Гав!")]
    LowAnimal,
    
    /// <summary>
    /// Зависит от животного
    /// </summary>
    [Description("Зависит от животного")]
    MiddleAnimal,
    
    /// <summary>
    /// Предпочитаю поездки без питомцев
    /// </summary>
    [Description("Предпочитаю поездки без питомцев")]
    HighAnimal,
}