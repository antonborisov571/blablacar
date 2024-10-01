namespace Blablacar.Contracts.Requests.Trips.GetTrip;

/// <summary>
/// Пассажир
/// </summary>
public class Passenger
{
    /// <summary>
    /// Id пассажира
    /// </summary>
    public string Id { get; set; } = default!;
    
    /// <summary>
    /// Имя пассажира
    /// </summary>
    public string FirstName { get; set; } = default!;

    /// <summary>
    /// Аватар
    /// </summary>
    public string Avatar { get; set; } = default!;
}