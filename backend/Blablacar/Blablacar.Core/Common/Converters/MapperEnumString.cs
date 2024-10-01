using System.ComponentModel;

namespace Blablacar.Core.Common.Converters;

/// <summary>
/// Метод расширения для маппинга строки в Enum и обратно
/// </summary>
public static class MapperEnumString
{
    /// <summary>
    /// Маппит Enum в строку
    /// </summary>
    /// <param name="value">Какой-то Enum</param>
    /// <returns>Строку Description</returns>
    /// <exception cref="ArgumentException">Передан не тот Enum</exception>
    public static string MappingEnum(Enum value) 
    {
        var fieldInfo = value.GetType().GetField(value.ToString());
        var attr = fieldInfo!
            .GetCustomAttributes(typeof(DescriptionAttribute), false)
            .FirstOrDefault();
        if (attr is null)
        {
            throw new ArgumentException($"Передан {value.GetType().FullName}, который не помечен атрибутом [Description()]");
        }
        return ((DescriptionAttribute)attr).Description;
    }
    
    /// <summary>
    /// Маппит строку в Enum
    /// </summary>
    /// <param name="value">Описание member-a Enum-a</param>
    /// <typeparam name="TEnum">Тип Enum в котором делаем маппинг</typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentException">Передан не тот Enum</exception>
    public static Enum MappingString<TEnum>(string value) where TEnum : Enum
    {
        var fieldsInfo = typeof(TEnum).GetFields();
        foreach (var fieldInfo in fieldsInfo.Where(f => f.FieldType != typeof(int)))
        {
            var attr = fieldInfo
                .GetCustomAttributes(typeof(DescriptionAttribute), false)
                .FirstOrDefault();
            if (attr is null)
            {
                throw new ArgumentException($"Передан {value.GetType().FullName}, который не помечен атрибутом [Description()]");
            }

            if (((DescriptionAttribute)attr).Description == value)
            {
                return (Enum)fieldInfo.GetValue(null)!;
            }
        }
        throw new ArgumentException($"Значение {value} не найдено в перечислении {typeof(TEnum).FullName}");
    }
}