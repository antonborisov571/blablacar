namespace Blablacar.Core.Enums;

/// <summary>
/// Стандартные сообщения об ошибках
/// </summary>
public static class BlablacarErrorMessages
{
    /// <summary>
    /// Пассажиров больше чем восемь
    /// </summary>
    public const string GreaterThanEight = "Пассажиров больше чем восемь";

    /// <summary>
    /// Пассажиров меньше чем один
    /// </summary>
    public const string LessThanOne = "Пассажиров меньше чем один";

    /// <summary>
    /// Время поездки должно быть позже, чем текущее время
    /// </summary>
    public const string DateLessThanNow = "Время поездки должно быть позже, чем текущее время";
    
    /// <summary>
    /// Пустое Required поле в Request
    /// </summary>
    /// <param name="fieldName"></param>
    /// <returns>Empty field error message</returns>
    public static string EmptyField(string fieldName) => $"{fieldName} не может быть пустым";
    
    /// <summary>
    /// Проверка даты
    /// </summary>
    /// <param name="date">Дата</param>
    /// <returns>Время поездки должно быть позже, чем текущее время</returns>
    public static bool IsDateValid(DateTime date) => 
        date > DateTime.Now;
}