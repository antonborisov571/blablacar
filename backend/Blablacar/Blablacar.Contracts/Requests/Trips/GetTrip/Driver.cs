using Blablacar.Contracts.Enums;

namespace Blablacar.Contracts.Requests.Trips.GetTrip;

/// <summary>
/// Водитель
/// </summary>
public class Driver
{
    /// <summary>
    /// Id водителя
    /// </summary>
    public string Id { get; set; } = default!;
    
    /// <summary>
    /// Имя водителя
    /// </summary>
    public string DriverName { get; set; } = default!;
    
    /// <summary>
    /// Аватар водителя
    /// </summary>
    public string? DriverAvatar { get; set; }
    
    /// <summary>
    /// Отношение к курению
    /// </summary>
    public SmokingType PreferencesSmoking { get; set; }
    
    /// <summary>
    /// Предпочтения о музыке
    /// </summary>
    public MusicType PreferencesMusic { get; set; }
    
    /// <summary>
    /// Предпочтения о животных
    /// </summary>
    public AnimalType PreferencesAnimal { get; set; }
}