using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Blablacar.Core.Common.Converters;

/// <summary>
/// Конвертер Enum-ов в строку и обратно
/// </summary>
/// <param name="mappingHints"><see cref="ConverterMappingHints"/></param>
/// <typeparam name="TEnum"><see cref="Enum"/></typeparam>
public class EnumStringConverter<TEnum>(
    ConverterMappingHints mappingHints = default!
    ) : ValueConverter<TEnum, string>(
    enumValue => MapperEnumString.MappingEnum(enumValue),
    stringValue => (TEnum)MapperEnumString.MappingString<TEnum>(stringValue),
    mappingHints) where TEnum : Enum;