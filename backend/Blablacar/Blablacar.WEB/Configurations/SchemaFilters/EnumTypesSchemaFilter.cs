using System.Xml.Linq;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Blablacar.WEB.Configurations.SchemaFilters;

/// <inheritdoc />
public class EnumTypesSchemaFilter : ISchemaFilter
{
    private readonly XDocument _xmlComments = null!;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="xmlPath">Путь до файла xml</param>
    public EnumTypesSchemaFilter(string xmlPath)
    {
        if (File.Exists(xmlPath))
        {
            _xmlComments = XDocument.Load(xmlPath);
        }
    }

    /// <inheritdoc />
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if(schema.Enum != null && schema.Enum.Count > 0 &&
           context.Type != null && context.Type.IsEnum)
        {
            schema.Description += "<ul>";

            var fullTypeName = context.Type.FullName;

            foreach (var enumMemberInt in schema.Enum.OfType<OpenApiInteger>().Select(v => v.Value))
            {
                var fullEnumMemberName = $"F:{fullTypeName}.{Enum.GetName(context.Type, enumMemberInt)}";

                var enumMemberComments = _xmlComments.Descendants("member")
                    .FirstOrDefault(m => m.Attribute("name")!.Value.Equals(fullEnumMemberName, StringComparison.OrdinalIgnoreCase));
                if (enumMemberComments == null) continue;

                var summary = enumMemberComments.Descendants("summary").FirstOrDefault();
                if (summary == null) continue;

                schema.Description += $"<li><i>{enumMemberInt}</i> - {Enum.GetName(context.Type, enumMemberInt)} - {summary.Value.Trim()}</li>";

            }

            schema.Description += "</ul>";
        }
    }
}